using MediatR;
using Notifications.Application.Abstractions;
using Notifications.Application.Handlers.Commands.Notifications;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Notifications
{
    public class CreateNotificationWithTemplateHandler
        : IRequestHandler<CreateNotificationWithTemplateCommand, Guid>
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly ITemplateRepository _templateRepo;
        private readonly ITemplateVersionRepository _templateVersionRepo;
        private readonly ITemplateVariableRepository _variableRepo;
        private readonly INotificationFactoryResolver _factoryResolver;

        public CreateNotificationWithTemplateHandler(
            INotificationRepository notificationRepo, 
            ITemplateRepository templateRepo, 
            ITemplateVersionRepository templateVersionRepo, 
            ITemplateVariableRepository variableRepo, 
            INotificationFactoryResolver factoryResolver)
        {
            _notificationRepo = notificationRepo;
            _templateRepo = templateRepo;
            _templateVersionRepo = templateVersionRepo;
            _variableRepo = variableRepo;
            _factoryResolver = factoryResolver;
        }

        public async Task<Guid> Handle(CreateNotificationWithTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _templateRepo.FindByCodeAsync(request.TemplateCode);
            if (template == null) 
                throw new ArgumentNullException("Template is not found");
            var templateVersion = _templateVersionRepo.GetByTemplateAndLanguageAsync(template.Id, request.Version, request.Language)
            if (templateVersion == null) 
                throw new ArgumentNullException("Template version is not found");

            var variables = await _variableRepo.GetAllByNamesAsync(request.Variables.Keys.ToList());


        }
    }
}
