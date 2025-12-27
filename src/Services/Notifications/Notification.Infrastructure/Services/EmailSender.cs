using Notifications.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsync(string to, string subject, string body, CancellationToken ct)
        {
            Console.WriteLine($"Sending email to: {to}, Subject: {subject}, Body: {body}");
            return Task.CompletedTask;
        }
    }
}
