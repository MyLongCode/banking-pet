using MediatR;
using Notifications.Application.Handlers.Commands.Templates.TemplateVariables;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.NotificationTemplates.TemplateVariables
{
    public class CreateTemplateVariableHandler : IRequestHandler<CreateTemplateVariableCommand, Guid>
    {
        private readonly ITemplateVariableRepository _repo;
        public CreateTemplateVariableHandler(ITemplateVariableRepository repo)
            => _repo = repo;

        public async Task<Guid> Handle(CreateTemplateVariableCommand request, CancellationToken cancellationToken)
        {
            return await _repo.AddAsync(new TemplateVariable
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
                Description = request.Description,
                ValidationRules = request.ValidationRules
            });
        }
    }
}
