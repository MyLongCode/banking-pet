using Microsoft.Extensions.Options;
using Notifications.Application.Abstractions;
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
    public sealed class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _opt;

        public SmtpEmailSender(IOptions<SmtpOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(string to, string subject, string body, CancellationToken ct)
        {
            using var client = new SmtpClient(_opt.Host, _opt.Port)
            {
                EnableSsl = _opt.UseSsl,
                Credentials = new NetworkCredential(_opt.User, _opt.Password),
            };

            using var message = new MailMessage(_opt.From, to, subject, body)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(message, ct);
        }
    }
}
