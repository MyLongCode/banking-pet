using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.NotificationFactories
{
    public class EmailNotificationFactory : INotificationFactory
    {
        private readonly IEmailSender _emailSender;

        public EmailNotificationFactory(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public Notification CreateNotification(
            NotificationType type, string recipient, string title, 
            string message, Dictionary<string, object>? metadata = null
            ) => new EmailNotification(recipient, title, message, metadata);

        public async Task SendAsync(Notification notification)
        {
            await _emailSender.SendAsync((EmailNotification)notification);
        }
    }
}
