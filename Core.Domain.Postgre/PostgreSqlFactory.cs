using Core.Domain.DI;
using Core.Domain.Masters.Database;
using Core.Domain.Postgre.Base;
using Core.Domain.Postgre.DI;
using Core.Domain.Postgre.Master;
using Core.Domain.Postgre.Providers;
using Core.Domain.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre
{
     public static class PostgreSqlFactory
    {
        public static void InjectDatabaseRepo(IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseRepo, DatabaseRepo>();
            services.AddSingleton<IBaseRepo, BaseRepo>();
            services.AddSingleton<IDatabaseProvider, PostgreProvider>();

            services.AddScoped<IStudentRepo, StudentRepo>();
        }

    }
}
