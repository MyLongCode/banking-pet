using MediatR;
using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Templates.TemplateVersions
{
    public record GetAllTemplateVersionsQuery(string TemplateCode) : IRequest<IEnumerable<TemplateVersion>>;
}
