using MediatR;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands
{
    public sealed record CreateNotificationCommand(
        NotificationType Type,
        string Recipient,
        string Title,
        string Message,
        List<string>? Metadata
    ) : IRequest<Guid>;
}
