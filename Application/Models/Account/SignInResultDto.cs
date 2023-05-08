namespace Application.Models.Account
{
    public class SignInResultDto : AuthTokenDto
    {
        public UserInfoDto? UserInfo { get; set; }
        public bool IsTFAEnabled { get; set; }
        public bool IsAuthSuccessful { get; set; }
    }
}
