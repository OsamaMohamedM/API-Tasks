using Authentication_Module.Domain.Entities;
using Authentication_Module.Domain.Interfaces;
using Authentication_Module.Domain.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Authentication_Module.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Password Reset Request";

            var resetLink = $"https://yourfrontend.com/reset-password?token={resetToken}";

            var builder = new BodyBuilder
            {
                HtmlBody = $"<p>Please reset your password by clicking <a href='{resetLink}'>here</a>.</p>"
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}