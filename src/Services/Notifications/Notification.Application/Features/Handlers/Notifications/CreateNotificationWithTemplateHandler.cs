using HandlebarsDotNet;
using MediatR;
using Notifications.Application.Abstractions;
using Notifications.Application.Handlers.Commands.Notifications;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Interfaces.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.Handlers.Notifications
{
    public class CreateNotificationWithTemplateHandler
        : IRequestHandler<CreateNotificationWithTemplateCommand, Guid>
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly ITemplateRepository _templateRepo;
        private readonly ITemplateVersionRepository _templateVersionRepo;
        private readonly ITemplateVariableRepository _variableRepo;
        private readonly INotificationFactoryResolver _factoryResolver;

        private const string TemplateHtml = @"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
          <meta charset='UTF-8'>
          <meta name='viewport' content='width=device-width, initial-scale=1.0'>
          <title>{{Title}}</title>
          <style>
            body {
              font-family: Arial, sans-serif;
              background-color: #f9f9f9;
              color: #333;
              margin: 0;
              padding: 0;
            }
            .container {
              max-width: 600px;
              margin: 0 auto;
              background-color: #ffffff;
              border-radius: 8px;
              box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
            }
            .header {
              background-color: #4CAF50;
              color: white;
              text-align: center;
              padding: 20px;
              border-top-left-radius: 8px;
              border-top-right-radius: 8px;
            }
            .footer {
              background-color: #f4f4f4;
              text-align: center;
              padding: 10px;
              border-bottom-left-radius: 8px;
              border-bottom-right-radius: 8px;
            }
            .content {
              padding: 20px;
            }
            h1 {
              font-size: 24px;
            }
            p {
              font-size: 16px;
              line-height: 1.5;
            }
            table {
              width: 100%;
              margin-top: 15px;
              border-collapse: collapse;
            }
            td {
              padding: 8px;
              border: 1px solid #ddd;
              background-color: #f9f9f9;
            }
            .important {
              color: #4CAF50;
              font-weight: bold;
            }
          </style>
        </head>
        <body>
          <div class='container'>
            <div class='header'>
              <h1>{{Title}}</h1>
            </div>
            <div class='content'>
              {{RenderedContent}}
            </div>
            <div class='footer'>
              <p>Банк 'ПетБанк' — это надежность и удобство для вас. Спасибо, что выбрали нас!</p>
            </div>
          </div>
        </body>
        </html>";

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

        public async Task<Guid> Handle(CreateNotificationWithTemplateCommand request, CancellationToken ct)
        {
            var template = await _templateRepo.FindByCodeAsync(request.TemplateCode);
            if (template == null)
                throw new ArgumentNullException("Template is not found");

            var templateVersion = await _templateVersionRepo.GetByTemplateAndLanguageAsync(
                template.Id, request.Version, request.Language);
            if (templateVersion == null)
                throw new ArgumentNullException("Template version is not found");

            var variables = await _variableRepo.GetAllByNamesAsync(request.Variables.Keys.ToList());
            var data = new Dictionary<string, object>();

            foreach (var variable in variables)
            {
                data[variable.Name] = request.Variables.ContainsKey(variable.Name)
                    ? request.Variables[variable.Name]
                    : null;
            }

            var handlebarsTemplate = Handlebars.Compile(templateVersion.Content.ToString());
            var renderedContent = handlebarsTemplate(data);

            var finalData = new Dictionary<string, object>
            {
                { "Title", template.Name },
                { "RenderedContent", renderedContent } 
            };

            var finalTemplate = Handlebars.Compile(TemplateHtml);
            var finalRenderedContent = finalTemplate(finalData);

            var factory = _factoryResolver.Resolve(NotificationType.Email);
            var notification = factory.CreateNotification(
                NotificationType.Email, request.To, template.Description, finalRenderedContent, null);

            notification.Validate();

            await factory.SendAsync(notification, ct);
            await _notificationRepo.AddAsync(notification, ct);

            return notification.Id;
        }
    }
}
