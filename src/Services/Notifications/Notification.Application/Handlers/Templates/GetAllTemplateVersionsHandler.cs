using MediatR;
using Notifications.Application.Handlers.Commands.Templates;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Templates
{
    public class GetAllTemplateVersionsHandler : IRequestHandler<GetAllTemplateVersionsQuery, IEnumerable<TemplateVersion>>
    {
        private readonly ITemplateVersionRepository _versionRepo;
        private readonly ITemplateRepository _templateRepo;
        public GetAllTemplateVersionsHandler(ITemplateVersionRepository versionRepo, ITemplateRepository templateRepo)
        {
            _versionRepo = versionRepo;
            _templateRepo = templateRepo;
        }
        public async Task<IEnumerable<TemplateVersion>> Handle(GetAllTemplateVersionsQuery request, CancellationToken cancellationToken)
        {
            var template = await _templateRepo.FindByCodeAsync(request.TemplateCode);
            if (template == null) throw new Exception("Template not found");

            return await _versionRepo.GetAllByTemplateIdAsync(template.Id);
        }
    }
}
