namespace Application.Models.Account
{
    public class UserForAuthenticationDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool KeepLogin { get; set; }
    }
}
