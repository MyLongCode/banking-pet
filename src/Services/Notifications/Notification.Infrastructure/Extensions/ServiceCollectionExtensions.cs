using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Notifications.Application.Abstractions;
using Notifications.Infrastructure.Options;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Repositories;
using Notifications.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
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

            services.Configure<SmtpOptions>(config.GetSection("Smtp"));

            services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();

            return services;
        }
    }
}
