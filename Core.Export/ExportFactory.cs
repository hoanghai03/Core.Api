using Core.Export.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Export.Services;

namespace Core.Export
{
    public static class ExportFactory
    {
        public static void InjectServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IExportService, ExportService>();
        }
    }
}
