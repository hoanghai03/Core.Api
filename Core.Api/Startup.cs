using Core.Domain.Shared.Configs;
using Core.Domain.Shared.Constants;
using Core.HostBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS
            services.AddCors();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core.Api", Version = "v1" });
            });

            #region Authen
            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(jwtOptions =>
            //{
            //    jwtOptions.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["JwtSettings:Issuer"],
            //        ValidateIssuer = true,
            //        ValidIssuer = Configuration["JwtSettings:Issuer"],
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"])),
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
            #endregion

            // inject filters
            HostBaseFactory.InjectActionFilterGlobal(services, Configuration);
            // inject 
            HostBaseFactory.InjectDatabaseService(services, Configuration);
            HostBaseFactory.InjectServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core.Api v1"));
            }
            //HostBaseFactory.ConfigureMidware(app);

            //var logger = loggerFactory.CreateLogger<Program>();
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    ExceptionModel exceptionModel = new ExceptionModel();
                    exceptionModel.userMsg = "HoangHai";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = Text.Plain;

                    //await context.Response.WriteAsync("An exception was thrown.");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is UnauthorizedAccessException)
                    {
                        exceptionModel.msgDev = "Bạn không có quyền truy cập";
                        exceptionModel.code = StatusCodes.Status401Unauthorized;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionModel));
                    }                    
                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        exceptionModel.msgDev = "File not found";
                        exceptionModel.code = StatusCodes.Status404NotFound;
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionModel));
                    }                    
                    if (exceptionHandlerPathFeature?.Error is Exception)
                    {
                        exceptionModel.msgDev = "Internal server error";
                        exceptionModel.code = StatusCodes.Status500InternalServerError;
                        //logger.LogInformation("Testing logging in Program.cs");
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionModel));
                    }                                       

                    if (exceptionHandlerPathFeature?.Path == "/")
                    {
                        await context.Response.WriteAsync(" Page: Home.");
                    }
                });
            });

            // Enable CORS
            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
