using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Abstractions;
using Notifications.Domain.Entities.Enums;

namespace Notifications.Application.NotificationFactories
{
    public class NotificationFactoryResolver : INotificationFactoryResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<NotificationType, Type> _factoryTypes;

        public NotificationFactoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _factoryTypes = new Dictionary<NotificationType, Type>();
        }

        public void RegisterFactory(NotificationType type, Type factoryType)
        {
            if (!typeof(INotificationFactory).IsAssignableFrom(factoryType))
            {
                throw new ArgumentException(
                    $"Type {factoryType.Name} must implement INotificationFactory");
            }

            _factoryTypes[type] = factoryType;
        }

        public INotificationFactory Resolve(NotificationType type)
        {
            if (!_factoryTypes.TryGetValue(type, out var factoryType))
            {
                throw new NotSupportedException(
                    $"Factory for {type} notification is not registered. " +
                    $"Registered types: {string.Join(", ", _factoryTypes.Keys)}");
            }

            return (INotificationFactory)_serviceProvider.GetRequiredService(factoryType);
        }
    }
}
