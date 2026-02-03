using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Notifications.Application.Abstractions;
using Notifications.Application.NotificationFactories;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Interfaces.NotificationTemplates;
using Notifications.Infrastructure.NotificationFactories;
using Notifications.Infrastructure.Options;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Repositories;
using Notifications.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationsInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<NotificationsDbContext>(opt =>
            {
                var cs = config.GetConnectionString("NotificationsDb");
                opt.UseNpgsql(cs);
            });
            services.AddScoped<IEmailSender, SmtpSender>();
            services.AddScoped<ITelegramSender, TelegramSender>();

            services.AddScoped<EmailNotificationFactory>();
            services.AddScoped<INotificationFactory, EmailNotificationFactory>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ITemplateVersionRepository, TemplateVersionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<INotificationFactoryResolver>(sp =>
            {
                var resolver = new NotificationFactoryResolver(sp);
                resolver.RegisterFactory(NotificationType.Email, typeof(EmailNotificationFactory));
                return resolver;
            });
            services.AddOptions<SmtpOptions>()
                .BindConfiguration("Smtp")
                .Validate(o => !string.IsNullOrWhiteSpace(o.From), "Smtp:From is required")
                .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "Smtp:Host is required")
                .ValidateOnStart();

            return services;
        }
    }
}
