using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Interfaces
{
    public interface INotificationFactory
    {
        Notification CreateNotification(
            NotificationType type,
            string recipient,
            string title,
            string message,
            Dictionary<string, object>? metadata = null
        );
    }
}
