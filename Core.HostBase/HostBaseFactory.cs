﻿using Core.Domain.Postgre;
using Core.HostBase.Filters;
using CORE.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
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

        public static void InjectActionFilterGlobal(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(
                options =>
                {
                    options.Filters.Add<CustomExceptionFilter>();
                }
            );

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new CustomExceptionFilter());
            //});

        }
    }
}
