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

namespace Notifications.Application.Handlers.Notifications
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationQuery, IEnumerable<Notification>>
    {
        private readonly INotificationRepository _repo;
        public GetNotificationHandler(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Notification>> Handle(GetNotificationQuery request, CancellationToken ct)
        {
            //добавить обработку дат
            return await _repo.GetByTypeAsync(request.Type, ct);
        }
    }
}
