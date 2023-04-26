using Application.Models;
using Application.Models.Account;
using Domain.Entities.User;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);

        Task<(Result Result, string UserId)> CreateUserAsync(UserForRegistrationDto userForReg);

        Task<Result> DeleteUserAsync(string userId);

        Task<SignInResultDto> AuthorizeAsync(UserForAuthenticationDto userForAuth , CancellationToken cancellationToken);
    }
}
