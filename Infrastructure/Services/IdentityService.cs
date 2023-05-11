using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using AutoMapper;
using Domain;
using Domain.Entities.User;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IdentityService(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IdentityService> logger,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Result> AssignToRoles(string userId, string[] roles)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            if (user != null)
            {
                await this.AssignToRoles(user, roles);
                return Result.Success();
            }
            return Result.Failure(new[] { "Invalid User Id" });
        }

        private async Task AssignToRoles(ApplicationUser user, string[] roles)
        {
            await _userManager.AddToRolesAsync(user, new[] { RoleList.Member.ToString() });
        }
        public async Task<SignInResultDto> AuthorizeAsync(UserForAuthenticationDto userForAuth)
        {
            // use Cookies
            // var result = await _signInManager.PasswordSignInAsync(userForAuth.UserName, userForAuth.Password, userForAuth.KeepLogin, true);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userForAuth.UserName);

            if (user != null)
            {
                // var result = await _signInManager.CheckPasswordSignInAsync(user, userForAuth.Password, true);
                // fix error: No authentication handler is registered for the scheme 'Identity.TwoFactorRememberMe'
                if (await _userManager.IsLockedOutAsync(user))
                {
                    throw new Exception($"The account is locked out due to multiple login attempts in {(await _userManager.GetLockoutEndDateAsync(user)).Value.Subtract(DateTimeOffset.UtcNow).Minutes} minutes");
                }
                var result = await _userManager.CheckPasswordAsync(user, userForAuth.Password ?? "");
                //if (result.Succeeded)
                if (result)
                {
                    if (_userManager.SupportsUserLockout)
                    {
                        var accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);
                        if (accessFailedCount > 0)
                            await _userManager.ResetAccessFailedCountAsync(user);
                    }
                    var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                    if (isTfaEnabled)
                    {
                        return new SignInResultDto
                        {
                            IsAuthSuccessful = true,
                            IsTFAEnabled = true
                        };
                    }
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateToken(user, roles);
					var refreshToken = GenerateRefreshToken();
					var data = new SignInResultDto
                    {
                        AccessToken = jwtToken,
                        UserInfo = _mapper.Map<UserInfoDto>(user),
                        IsAuthSuccessful = true,
                        IsTFAEnabled = false,
                        RefreshToken = refreshToken
                    };
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddHours(userForAuth.KeepLogin ? int.Parse(_configuration["JwtSettings:keepLoginRefreshTokenValidityInHours"] ?? "720") : int.Parse(_configuration["JwtSettings:defaultRefreshTokenValidityInHours"] ?? "24"));
                    user.LastLoginDate = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                    return data;
                }
                else
                {
                    if (_userManager.SupportsUserLockout && await _userManager.GetLockoutEnabledAsync(user))
                    {
                        await _userManager.AccessFailedAsync(user);
                    }
                }
                //if (result.IsLockedOut)
                //{

                //    throw new Exception($"The account is locked out due to multiple login attempts in {(await _userManager.GetLockoutEndDateAsync(user)).Value.Subtract(DateTimeOffset.UtcNow).Minutes} minutes");
                //}
            }

            throw new Exception("Invalid Password");
        }
        public async Task<SignInResultDto> AuthorizeTFAAsync(UserForTFAAuthDto userForTFAAuth)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _userManager.FindByNameAsync(userForTFAAuth.Email);
#pragma warning restore CS8604 // Possible null reference argument.
            if (user == null)
                throw new Exception("Invalid Authentication");
#pragma warning disable CS8604 // Possible null reference argument.
            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(
                 user, _userManager.Options.Tokens.AuthenticatorTokenProvider, userForTFAAuth.Code);
#pragma warning restore CS8604 // Possible null reference argument.
            if (!validVerification)
                throw new Exception("Invalid Token Verification");
            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = _tokenService.CreateToken(user, roles);
			var refreshToken = GenerateRefreshToken();
			var data = new SignInResultDto
            {
                AccessToken = jwtToken,
                UserInfo = _mapper.Map<UserInfoDto>(user),
                IsAuthSuccessful = true,
                IsTFAEnabled = true,
				RefreshToken = refreshToken
			};
			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddHours(userForTFAAuth.KeepLogin ? int.Parse(_configuration["JwtSettings:keepLoginRefreshTokenValidityInHours"] ?? "720") : int.Parse(_configuration["JwtSettings:defaultRefreshTokenValidityInHours"] ?? "24"));
			user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return data;
        }
        public async Task<(Result Result, string UserId)> CreateUserAsync(UserForRegistrationDto userForRegistration, string[] roles)
        {
            var user = _mapper.Map<ApplicationUser>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
            {
                await this.AssignToRoles(user, roles);
            }

            return (result.ToApplicationResult(), user.Id);
        }
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }
        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<TFASetupDto> GetTFASetupAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (authenticatorKey == null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var formattedKey = GenerateQRCode(user.Email, authenticatorKey);
#pragma warning restore CS8604 // Possible null reference argument.
            return new TFASetupDto { IsTfaEnabled = isTfaEnabled, AuthenticatorKey = authenticatorKey, FormattedKey = formattedKey };
        }
        private string GenerateQRCode(string email, string unformattedKey)
        {
            return string.Format(
            Constants.AuthenticatorUriFormat,
                System.Web.HttpUtility.UrlEncode("Net Core API Two-Factor Auth"),
                System.Web.HttpUtility.UrlEncode(email),
                unformattedKey);
        }
        public async Task SetupTFAAsync(string userId, string code, bool isEnable)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var isValidCode = await _userManager
                .VerifyTwoFactorTokenAsync(user,
                  _userManager.Options.Tokens.AuthenticatorTokenProvider,
                  code);
            if (!isValidCode)
            {
                throw new Exception("Invalid Token Verification");
            }
            await _userManager.SetTwoFactorEnabledAsync(user, isEnable);
        }
        public async Task<UserForResetPasswordDto> GetTokenPasswordResetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            return new UserForResetPasswordDto()
            {
                UserId = user.Id,
                Token = await _userManager.GeneratePasswordResetTokenAsync(user)
            };
        }
        public async Task ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(',', result.Errors.ToList().Select(x => x.Description)));
            }
        }
        private string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
		public async Task<AuthTokenDto> RefreshTokenAsync(string accessToken, string refreshToken)
		{
            var principal = _tokenService.DecryptTokenToClaim(accessToken, false);
			if (principal == null)
			{
				throw new Exception("Invalid access token");
			}
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			var userId = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
			var user = await _userManager.FindByIdAsync(userId);
#pragma warning restore CS8604 // Possible null reference argument.

			if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTimeOffset.UtcNow)
			{
				throw new Exception("Refresh token is invalid or expiry");
			}
			var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _tokenService.CreateToken(user, roles);
			var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            return new AuthTokenDto
            {
                RefreshToken = newRefreshToken,
                AccessToken = newAccessToken
            };
		}
        public async Task<ChangePasswordResponseDto> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (isTfaEnabled)
            {
                return new ChangePasswordResponseDto
                {
                    IsPasswordChanged = false,
                    IsTFAEnabled = true
                };
            }
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(',', result.Errors.ToList().Select(x => x.Description)));
            }
            return new ChangePasswordResponseDto
            {
                IsPasswordChanged = true,
                IsTFAEnabled = false
            };
        }
        public async Task<ChangePasswordResponseDto> ChangePasswordWithTFAAsync(string userId, string currentPassword, string newPassword, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(
                 user, _userManager.Options.Tokens.AuthenticatorTokenProvider, code);
            if (!validVerification)
                throw new Exception("Invalid Token Verification");
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(',', result.Errors.ToList().Select(x => x.Description)));
            }
            return new ChangePasswordResponseDto
            {
                IsPasswordChanged = true,
                IsTFAEnabled = true
            };
        }
        public async Task<SignInResultDto> GoogleLoginAsync(ExternalAuthDto externalAuth)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var payload = await _tokenService.VerifyGoogleTokenAsync(externalAuth.IdToken);
#pragma warning restore CS8604 // Possible null reference argument.

            if (payload == null)
                throw new Exception("Invalid External Authentication.");
#pragma warning disable CS8604 // Possible null reference argument.
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
#pragma warning restore CS8604 // Possible null reference argument.
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser { Email = payload.Email, UserName = payload.Email, FirstName = payload.GivenName, LastName = payload.FamilyName };
                    await _userManager.CreateAsync(user);
                    //prepare and send an email for the email confirmation
                    await _userManager.AddToRoleAsync(user, RoleList.Member.ToString());
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            // should we check for the Locked out account???
            
            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = _tokenService.CreateToken(user, roles);
            var refreshToken = GenerateRefreshToken();
            var data = new SignInResultDto
            {
                AccessToken = jwtToken,
                UserInfo = _mapper.Map<UserInfoDto>(user),
                IsAuthSuccessful = true,
                RefreshToken = refreshToken
            };
            user.RefreshToken = refreshToken;
            if (payload.ExpirationTimeSeconds.HasValue)
            {
                user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddSeconds(payload.ExpirationTimeSeconds.Value);
            }
            else
            {
                user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddHours(int.Parse(_configuration["JwtSettings:defaultRefreshTokenValidityInHours"] ?? "24"));
            }
            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return data;
        }
    }
}
