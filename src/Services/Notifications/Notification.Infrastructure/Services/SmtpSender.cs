using Microsoft.Extensions.Options;
using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Services
{
    public sealed class SmtpSender : IEmailSender
    {
        private readonly SmtpOptions _opt;

        public SmtpSender(IOptions<SmtpOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(EmailNotification notification)
        {
            using var client = new SmtpClient(_opt.Host, _opt.Port)
            {
                EnableSsl = _opt.UseSsl,
                Credentials = new NetworkCredential(_opt.User, _opt.Password),
            };

            using var message = new MailMessage(_opt.From, notification.Recipient, notification.Subject, notification.Message)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
        }
    }
}
