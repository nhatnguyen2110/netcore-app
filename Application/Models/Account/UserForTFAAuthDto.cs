namespace Application.Models.Account
{
    public class UserForTFAAuthDto
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
        public bool KeepLogin { get; set; }
    }
}
