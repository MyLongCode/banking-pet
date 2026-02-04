using MediatR;
using Notifications.Application.Handlers.Commands.NotificationsTemplates.TemplateVariables;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.NotificationTemplates.TemplateVariables
{
    public class GetTemplateVariableByNameHandler : IRequestHandler<GetTemplateVariableByNameQuery, TemplateVariable?>
    {
        private readonly ITemplateVariableRepository _repo;
        public GetTemplateVariableByNameHandler(ITemplateVariableRepository repo) 
            => _repo = repo;
        public async Task<TemplateVariable?> Handle(GetTemplateVariableByNameQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByNameAsync(request.Name, cancellationToken);
        }
    }
}
