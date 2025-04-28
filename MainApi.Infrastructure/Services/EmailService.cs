using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account.ForgotPassword;
using MainApi.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MainApi.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _fromEmail = configuration["EmailSettings:FromEmail"] ?? string.Empty;
            _fromName = configuration["EmailSettings:FromName"] ?? string.Empty;
            _smtpPassword = configuration["EmailSettings:SmtpPassword"] ?? string.Empty;
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] ?? string.Empty);
            _smtpServer = configuration["EmailSettings:SmtpServer"] ?? string.Empty;
            _smtpUsername = configuration["EmailSettings:SmtpUsername"] ?? string.Empty;
        }


        public async Task<bool> SendPasswordResetEmail(SendPasswordResetEmailDto passwordResetEmailDto)
        {
            SmtpClient smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_fromEmail, _smtpPassword),
                EnableSsl = true
            };
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(_fromEmail, _fromName),
                Subject = "Password Reset Request",
                Body = $"This is your token to change your password \n {passwordResetEmailDto.ResetLink}",
                IsBodyHtml = false
                // Body = $"Click here to reset your password: <a href='{passwordResetEmailDto.ResetLink}'>Reset Password</a>",
            };
            mailMessage.To.Add(passwordResetEmailDto.ToEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex}");

                return false;
            }
        }
    }
}