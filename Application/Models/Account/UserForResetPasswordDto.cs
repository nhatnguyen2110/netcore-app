namespace Application.Models.Account
{
    public class UserForResetPasswordDto
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }
}
