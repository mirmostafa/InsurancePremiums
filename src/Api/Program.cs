using Autofac;

using Infrastructure.Bcl.Helpers;
using Infrastructure.Cqrs.Models.Queries;

using Service;
using Service.Domain.Dtos;
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

app.MapGet("/ip/coverage", async () =>
{
    using var scope = container.BeginLifetimeScope();
    var queryProcessor = scope.Resolve<IQueryProcessor>();
    var result = await queryProcessor.ExecuteAsync(new GetAllCoveragesQueryParams());
    return result.Coverages.ThrowOnFail().GetValue();
});
app.MapGet("/ip/coverage/{id:int}", (int id) => "Hello World!");

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.Run();