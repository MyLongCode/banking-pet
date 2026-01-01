using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.UseCases
{
    public class CreateNotificationUseCase
    {
        private INotificationRepository _repo;
        private INotificationFactoryResolver _factoryResolver;

        public CreateNotificationUseCase(INotificationRepository repo, INotificationFactoryResolver factoryResolver)
        {
            _repo = repo;
            _factoryResolver = factoryResolver;
        }

        public async Task<Notification> Execute(
            NotificationType type,
            string recipient,
            string title,
            string message,
            Dictionary<string, object>? metadata = null)
        {
            var factory = _factoryResolver.Resolve(type);
            var notification = factory.CreateNotification(
                type, recipient, title, message, metadata);

            notification.Validate();
            await factory.SendAsync(notification);

            await _repo.AddAsync(notification);

            return notification;
        }
    }
}
