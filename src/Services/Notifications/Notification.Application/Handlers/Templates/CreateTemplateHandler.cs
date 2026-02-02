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
    public class CreateTemplateHandler : IRequestHandler<CreateTemplateCommand, Guid>
    {
        private readonly ITemplateRepository _templateRepository;
        public CreateTemplateHandler(ITemplateRepository templateRepository) 
            => _templateRepository = templateRepository;

        public async Task<Guid> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = new NotificationTemplate
            {
                Category = request.Category,
                Code = request.Code,
                IsActive = false,
                Name = request.Name,
                DefaultLanguage = request.DefaultLanguage,
                Description = request.Description,
                Created = DateTime.Now
            };
            await _templateRepository.AddAsync(template);
            return template.Id;
        }
    }
}
