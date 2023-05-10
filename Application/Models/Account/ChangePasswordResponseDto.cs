namespace Application.Models.Account
{
    public class ChangePasswordResponseDto
    {
        public bool IsPasswordChanged { get; set; }
        public bool IsTFAEnabled { get; set; }
    }
}
