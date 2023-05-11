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
        Task<SignInResultDto> AuthorizeTFAAsync(UserForTFAAuthDto userForTFAAuth);
        Task<Result> AssignToRoles(string userId, string[] roles);
        Task<TFASetupDto> GetTFASetupAsync(string userId);
        Task SetupTFAAsync(string userId, string code, bool isEnable);
        Task<ChangePasswordResponseDto> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<ChangePasswordResponseDto> ChangePasswordWithTFAAsync(string userId, string currentPassword, string newPassword, string code);
        Task<UserForResetPasswordDto> GetTokenPasswordResetAsync(string email);
        Task ResetPasswordAsync(string userId, string token, string newPassword);
		Task<AuthTokenDto> RefreshTokenAsync(string accessToken, string refreshToken);
	}
}
