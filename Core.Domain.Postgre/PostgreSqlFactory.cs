using Core.Domain.Masters.Database;
using Core.Domain.Postgre.Master;
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
        }
    }
}
