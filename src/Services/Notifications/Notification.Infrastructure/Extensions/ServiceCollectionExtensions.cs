using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Notifications.Application.Abstractions;
using Notifications.Application.NotificationFactories;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
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
            services.AddScoped<EmailNotificationFactory>();
            services.Configure<SmtpOptions>(config.GetSection("Smtp"));
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationFactoryResolver>(sp =>
            {
                var resolver = new NotificationFactoryResolver(sp);

                resolver.RegisterFactory(NotificationType.Email, typeof(EmailNotificationFactory));

                return resolver;
            });

            return services;
        }
    }
}
