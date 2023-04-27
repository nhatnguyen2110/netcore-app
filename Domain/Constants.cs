namespace Domain
{
    public static class Constants
    {
        public static string GeneralErrorMessage = "Something went wrong. Please try again";
        public static string CorsPolicy = "CorsPolicy";
        public static string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
    }
}
