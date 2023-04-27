namespace Application.Models.Account
{
    public class TFASetupDto
    {
        public bool IsTfaEnabled { get; set; }
        public string? AuthenticatorKey { get; set;}
        public string? FormattedKey { get; set; }
    }
}
