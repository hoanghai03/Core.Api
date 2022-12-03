using CORE.Application.Contracts.DI;
using CORE.Application.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application
{
    public static class ApplicationFactory
    {
        public static void InjectServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}
