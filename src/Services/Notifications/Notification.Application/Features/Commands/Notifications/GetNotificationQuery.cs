using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands
{
    public sealed record GetNotificationQuery
    (
        NotificationType Type,
        DateTime From,
        DateTime To
    ) : IRequest<IEnumerable<Notification>>;
}
