using MediatR;
using Notifications.Domain.Entities.NotificationTemplates.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Templates.NotificationTemplates
{
    public sealed record CreateTemplateCommand(
        string Code,
        string Name,
        string Description,
        NotificationTemplateCategory Category,
        string DefaultLanguage
        ) : IRequest<Guid>;
}
