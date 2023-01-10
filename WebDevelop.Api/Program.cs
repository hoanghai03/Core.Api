using Core.Domain.Shared.Constants;
using Core.HostBase;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inject filters
HostBaseFactory.InjectActionFilterGlobal(builder.Services, builder.Configuration);
// inject 
HostBaseFactory.InjectDatabaseService(builder.Services, builder.Configuration);
HostBaseFactory.InjectServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
