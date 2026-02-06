using MediatR;
using Notifications.Application.Handlers.Commands.Templates.NotificationTemplates;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.Handlers.NotificationTemplates.Templates
{
    public class GetAllTemplatesHandler : IRequestHandler<GetAllTemplatesQuery, IEnumerable<NotificationTemplate>>
    {
        private readonly ITemplateVersionRepository _versionRepo;
        private readonly ITemplateRepository _templateRepo;
        public GetAllTemplatesHandler(ITemplateVersionRepository versionRepo, ITemplateRepository templateRepo)
        {
            _versionRepo = versionRepo;
            _templateRepo = templateRepo;
        }
        public async Task<IEnumerable<NotificationTemplate>> Handle(GetAllTemplatesQuery request, CancellationToken cancellationToken)
        {
            return await _templateRepo.GetAllAsync();
        }
    }
}
