using MediatR;
using Notifications.Application.Handlers.Commands;
using Notifications.Application.Handlers.Commands.Templates.NotificationTemplates;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.Handlers.NotificationTemplates.Templates
{
    public class GetTemplateHandler : IRequestHandler<GetTemplateQuery, TemplateVersion>
    {
        private readonly ITemplateVersionRepository _versionRepo;
        private readonly ITemplateRepository _templateRepo;
        public GetTemplateHandler(ITemplateVersionRepository versionRepo, ITemplateRepository templateRepo)
        {
            _versionRepo = versionRepo;
            _templateRepo = templateRepo;
        }
        public async Task<TemplateVersion> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException("Request is null");
            if (request.TemplateCode == null || request.Language == null)
                throw new ArgumentNullException("TemplateCode or Language in request is null");
            var template = await _templateRepo.FindByCodeAsync(request.TemplateCode);
            return await _versionRepo.GetByTemplateAndLanguageAsync(template.Id, request.Version, request.Language);
        }
    }
}
