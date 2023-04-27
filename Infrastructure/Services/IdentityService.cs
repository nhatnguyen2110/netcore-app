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
using System.Text.Encodings.Web;

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
            _signInManager= signInManager;
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
                await this.AssignToRoles(user,roles);
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
                var result = await _signInManager.CheckPasswordSignInAsync(user, userForAuth.Password, true);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateToken(user, roles, userForAuth.KeepLogin);
                    var data = new SignInResultDto
                    {
                        AccessToken = jwtToken,
                        UserInfo = _mapper.Map<UserInfoDto>(user)
                    };
                    user.LastLoginDate = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                    return data;
                }
                if (result.IsLockedOut)
                {
                    throw new Exception("The account is locked out");
                }
            }
            
            throw new Exception("Invalid UserName or Password");
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
    }
}
