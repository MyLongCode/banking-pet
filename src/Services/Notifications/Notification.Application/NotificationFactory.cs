using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification CreateNotification(NotificationType type, string recipient, string title, string message, Dictionary<string, object>? metadata = null)
        {
            
        }

        static NotificationFactory GetNotificationFactory(string notificationType)
        {
            var factoryName = $"FactoryMethod.Factories.{notificationType}MessageFactory";
            var assembly = Assembly.GetExecutingAssembly();
            Type factoryType = assembly.GetType(factoryName);
            if (factoryType == null) throw new NotSupportedException($"Factory for {notificationType} notification is not found");
            return (NotificationFactory)Activator.CreateInstance(factoryType);
        }
    }
}
