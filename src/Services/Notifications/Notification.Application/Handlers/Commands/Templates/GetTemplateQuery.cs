using MediatR;
using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Templates
{
    public record GetTemplateQuery(
        string TemplateCode,
        string Version,
        string Language) : IRequest<TemplateVersion>;
}
