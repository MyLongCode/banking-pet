using MediatR;
using Notifications.Application.Abstractions;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationCommand, IEnumerable<Notification>>
    {
        private readonly INotificationRepository _repo;
        private readonly INotificationFactoryResolver _factoryResolver;

        public GetNotificationHandler(INotificationRepository repo, INotificationFactoryResolver factoryResolver)
        {
            _repo = repo;
            _factoryResolver = factoryResolver;
        }

        public async Task<IEnumerable<Notification>> Handle(GetNotificationCommand request, CancellationToken ct)
        {
            //добавить обработку дат
            return (await _repo.GetByTypeAsync(request.Type, ct));
        }
    }
}
