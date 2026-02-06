using MediatR;
using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.NotificationsTemplates.TemplateVariables
{
    public record GetTemplateVariablesQuery() : IRequest<IEnumerable<TemplateVariable>>;
}
