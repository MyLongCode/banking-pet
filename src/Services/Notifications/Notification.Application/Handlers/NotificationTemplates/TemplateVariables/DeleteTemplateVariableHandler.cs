using MediatR;
using Notifications.Application.Handlers.Commands.NotificationsTemplates.TemplateVariables;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.NotificationTemplates.TemplateVariables
{
    public class DeleteTemplateVariableHandler 
        : IRequestHandler<DeleteTemplateVariableCommand, Guid>
    {
        private readonly ITemplateVariableRepository _repo;
        public DeleteTemplateVariableHandler(ITemplateVariableRepository repo)
            => _repo = repo;

        async Task<Guid> IRequestHandler<DeleteTemplateVariableCommand, Guid>.Handle(DeleteTemplateVariableCommand request, CancellationToken cancellationToken)
        {
            var variable = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (variable == null)
            {
                throw new KeyNotFoundException($"Template variable with id {request.Id} not found.");
            }
            await _repo.DeleteAsync(variable, cancellationToken);
            return request.Id;  
        }
    }
}
