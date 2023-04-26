namespace Application.Models.Account
{
    public class UserTokenDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int ExpireInMinutes { get; set; }
    }
}
