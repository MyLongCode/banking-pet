using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.NotificationFactories
{
    public abstract class NotificationFactory : INotificationFactory
    {
        public abstract Notification CreateNotification(
            NotificationType type,
            string recipient,
            string title,
            string message,
            List<string>? metadata = null);

        public abstract Task SendAsync(Notification notification, CancellationToken ct);
    }
}
