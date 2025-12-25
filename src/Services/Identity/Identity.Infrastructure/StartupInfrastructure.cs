using Identity.Infrastructure.EF;
using Identity.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    internal class StartupInfrastructure
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork<IdentityDbContext>>();
        }
    }
}
