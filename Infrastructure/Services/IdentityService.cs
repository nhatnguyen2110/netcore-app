using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using AutoMapper;
using Domain.Entities.User;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private ApplicationUser _user;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IdentityService(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            UserManager<ApplicationUser> userManager,
            ILogger<IdentityService> logger,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<SignInResultDto> AuthorizeAsync(UserForAuthenticationDto userForAuth, CancellationToken cancellationToken)
        {
           
            if (await this.ValidateUser(userForAuth))
            {
                var roles = await _userManager.GetRolesAsync(_user);
                var jwtToken = _tokenService.CreateToken(_user, roles, userForAuth.KeepLogin);
            }
            throw new Exception("Login Failed");
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<ApplicationUser>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

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

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            _user = await _userManager.FindByNameAsync(userForAuth.UserName ?? "");
#pragma warning restore CS8601 // Possible null reference assignment.
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password ?? ""));
        }
    }
}
