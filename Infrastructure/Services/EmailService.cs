using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string subject, string emailContent, string emailTo, string? cc = null, string? bcc = null, string? displayName = null, Attachment[]? attachments = null)
        {
            var host = _configuration["SMTPConfig:Host"] ?? "";
            var port = _configuration["SMTPConfig:Port"] ?? "";
            var ssl = _configuration["SMTPConfig:SSL"] ?? "";
            var userName = _configuration["SMTPConfig:UserName"] ?? "";
            var password = _configuration["SMTPConfig:Password"] ?? "";
            CommonHelper.SendMail(host, port, ssl, userName, password, userName, displayName, subject, emailContent, emailTo, cc, bcc, attachments);
        }
    }
}
