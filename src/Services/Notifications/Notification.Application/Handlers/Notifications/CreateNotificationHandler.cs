using MediatR;
using Notifications.Application.Abstractions;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Notifications
{
    public sealed class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly INotificationRepository _repo;
        private readonly INotificationFactoryResolver _factoryResolver;

        public CreateNotificationHandler(INotificationRepository repo, INotificationFactoryResolver factoryResolver)
        {
            _repo = repo;
            _factoryResolver = factoryResolver;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken ct)
        {
            var factory = _factoryResolver.Resolve(request.Type);
            var notification = factory.CreateNotification(
                request.Type, request.Recipient, request.Title, request.Message, request.Metadata);

            notification.Validate();

            await factory.SendAsync(notification, ct);
            await _repo.AddAsync(notification, ct);

            return notification.Id;
        }
    }
}
