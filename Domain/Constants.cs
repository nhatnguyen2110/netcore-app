namespace Domain
{
    public static class Constants
    {
        public static class ApiVersionName
        {
            public const string Name = "Net Core Web API";
            public const int MajorVersion = 1;
            public const int MinorVersion = 0;

        }
        public static string GeneralErrorMessage = "Something went wrong. Please try again";
        public static string CorsPolicy = "CorsPolicy";
        public static string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
    }
}
