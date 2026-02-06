using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Templates.TemplateVariables
{
    public record CreateTemplateVariableCommand(
        string Name,
        string DisplayName,
        string Description,
        JsonElement ValidationRules) : IRequest<Guid>;
}
