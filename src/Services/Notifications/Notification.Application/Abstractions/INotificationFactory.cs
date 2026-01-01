using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Abstractions
{
    public interface INotificationFactory
    {
        public Notification CreateNotification(
            NotificationType type,
            string recipient,
            string title,
            string message,
            Dictionary<string, object>? metadata = null);

        public Task SendAsync(Notification notification);
    }
}