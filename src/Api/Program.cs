using Api.Controllers;

using Autofac;

using Microsoft.AspNetCore.Diagnostics;

using Service;
using Service.Infrastructure.Cqrs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddApplicationServices();

var containerBuilder = new ContainerBuilder();
containerBuilder.AddCqrs(typeof(Service.Startup).Assembly);
containerBuilder.AddApplicationServices();
var container = containerBuilder.Build();

var app = builder.Build();
CoverageAPIs.MapApis(app, container);

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";

        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (errorFeature != null)
        {
            var error = errorFeature.Error;
            await context.Response.WriteAsync($"Error: {error.Message}");
        }
        else
        {
            await context.Response.WriteAsync("An error occurred.");
        }
    });
});

app.Run();