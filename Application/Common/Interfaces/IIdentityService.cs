using Application.Models;
using Application.Models.Account;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<(Result Result, string UserId)> CreateUserAsync(UserForRegistrationDto userForReg, string[] roles);

        Task<Result> DeleteUserAsync(string userId);

        Task<SignInResultDto> AuthorizeAsync(UserForAuthenticationDto userForAuth);
        Task<Result> AssignToRoles(string userId, string[] roles);
        Task<TFASetupDto> GetTFASetupAsync(string userId);
    }
}
