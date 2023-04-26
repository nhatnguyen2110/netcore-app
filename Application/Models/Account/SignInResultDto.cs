namespace Application.Models.Account
{
    public class SignInResultDto
    {
        public string? AccessToken { get; set; }
        public UserInfoDto? UserInfo { get; set; }

    }
}
