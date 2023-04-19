namespace Application.Models.Settings
{
    public class ApplicationSettings
    {
        public string? PrivateKeyEncode { get; set; }
        public string? PublicKeyEncode { get; set; }
        public string? EncryptPassword { get; set; }
        public bool IsUseSwagger { get; set; }
        public bool IsAddSecurityHeaders { get; set; }
        public int MaxLoginFailedCount { get; set; } = 5;
        public int LockoutDurationInMinutes { get; set; } = 15;
        public string? SMTPDisplayName { get; set; }
        public string? SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public bool SMTPSSL { get; set; }
        public string? SMTPUserName { get; set; }
        public string? SMTPPassword { get; set; }
        public bool EnableEmailConfirmForRegister { get; set; }
        public int ExpireEmailConfirmInMinutes { get; set; } = 43200;
        public string? EmailConfirm_Subject { get; set; }
        public string? EmailConfirm_Content { get; set; }
        public int ExpireEmailResetPasswordInMinutes { get; set; } = 30;
        public string? EmailResetPassword_Subject { get; set; }
        public string? EmailResetPassword_Content { get; set; }
        public int ExpireEmailVerificationCodeInSeconds { get; set; } = 60;
        public string? EmailVerificationCode_Subject { get; set; }
        public string? EmailVerificationCode_Content { get; set; }
        public bool EnableEncryptAuthorize { get; set; }
        public bool EnableGoogleReCaptcha { get; set; }
        public string? GoogleRecaptchaVersion { get; set; } = "v2"; //v2,v3
        public string? GoogleSiteKey { get; set; }
        public string? GoogleSecretKey { get; set; }

    }
}
