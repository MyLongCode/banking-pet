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
            services.AddScoped(typeof(GenericRepository<>));
        }
    }
}
