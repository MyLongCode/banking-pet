using MediatR;
using Notifications.Application.Handlers.Commands.Templates.TemplateVersions;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.NotificationTemplates.TemplateVersions
{
    public class CreateTemplateVersionHandler : IRequestHandler<CreateTemplateVersionCommand, Guid>
    {
        private readonly ITemplateVersionRepository _repo;
        public CreateTemplateVersionHandler(ITemplateVersionRepository repo)
            => _repo = repo;

        public async Task<Guid> Handle(CreateTemplateVersionCommand request, CancellationToken cancellationToken)
        {
            var version = new TemplateVersion
            {
                TemplateId = request.TemplateId,
                Version = request.Version,
                Language = request.Language,
                Category = request.Category,
                Content = request.Content,
                Created = DateTime.UtcNow
            };
            await _repo.AddAsync(version);
            return version.Id;
        }
    }
}
