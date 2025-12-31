using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.UseCases
{
    public class CreateNotificationUseCase
    {
        private INotificationFactory _factory;
        private INotificationRepository _repo;

        public CreateNotificationUseCase(INotificationFactory factory, INotificationRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public async Task<Notification> Execute(
            NotificationType type,
            string recipient,
            string title,
            string message,
            Dictionary<string, object>? metadata = null)
        {
            var notification = _factory.CreateNotification(
                type, recipient, title, message, metadata);

            notification.Validate();

            await _repo.AddAsync(notification);

            return notification;
        }
    }
}
