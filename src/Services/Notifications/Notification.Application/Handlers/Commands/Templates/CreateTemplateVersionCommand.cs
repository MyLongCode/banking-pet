using MediatR;
using Notifications.Domain.Entities.NotificationTemplates.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Templates
{
    public sealed record CreateTemplateVersionCommand(
        Guid TemplateId,
        string Version,
        string Language,
        JsonElement Content,
        NotificationTemplateVersionCategory Category
        ) : IRequest<Guid>;
}
