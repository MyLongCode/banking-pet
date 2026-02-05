using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Notifications
{
    public record CreateNotificationWithTemplateCommand(
        string To,
        string TemplateCode,
        string Version,
        string Language,
        Dictionary<string, object> Variables
        ) : IRequest<Guid>;
}
