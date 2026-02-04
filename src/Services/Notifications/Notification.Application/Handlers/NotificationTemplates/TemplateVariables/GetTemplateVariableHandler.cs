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
    public class GetTemplateVariableHandler
        : IRequestHandler<GetTemplateVariablesQuery, IEnumerable<TemplateVariable>>
    {
        private readonly ITemplateVariableRepository _repo;
        public GetTemplateVariableHandler(ITemplateVariableRepository repo)
            => _repo = repo;
        public async Task<IEnumerable<TemplateVariable>> Handle(GetTemplateVariablesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}
