using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MemoryCache
{
    public static class CacheFactory
    {
        public static void InjectServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICached, BaseCached>();
        }
    }
}
