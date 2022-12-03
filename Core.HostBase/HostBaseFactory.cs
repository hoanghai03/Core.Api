using Core.Domain.Postgre;
using CORE.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HostBase
{
    public static class HostBaseFactory
    {
        /// <summary>
        /// inject database
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InjectDatabaseService(IServiceCollection services,IConfiguration configuration)
        {
            PostgreSqlFactory.InjectDatabaseRepo(services, configuration);
        } 

        /// <summary>
        /// inject service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InjectServices(IServiceCollection services,IConfiguration configuration)
        {
            ApplicationFactory.InjectServices(services, configuration);
        }
    }
}
