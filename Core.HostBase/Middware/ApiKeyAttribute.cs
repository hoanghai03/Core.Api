using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HostBase.Middware
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : System.Attribute, IAsyncActionFilter
    {
        private const string _apiKey = "x-api-key";


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var extractedApiKey))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("No API key was provided.");
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>(_apiKey);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Invalid API key.");
                return;
            }

            await next();
        }
    }
}
