using Autofac;

using Infrastructure.Bcl.Helpers;
using Infrastructure.Cqrs.Models.Queries;

using Service.Domain.Entities;

namespace Api.Controllers;

public static class CoverageAPIs
{
    public static WebApplication MapApis(WebApplication app, IContainer container)
    {
        _ = app.MapGet("/ip/coverage", async () =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetAllCoveragesQueryParams());
            return result.Result.ThrowOnFail().GetValue();
        });

        _ = app.MapGet("/ip/coverage/{id:guid}", async (Guid id) =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetCoverageByIdQueryParams(id));
            return result.Result.ThrowOnFail().GetValue();
        });
        _ = app.MapGet("/ip/coverage/{name}", async (string name) =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetCoverageByNameQueryParams(name));
            return result.Result.ThrowOnFail().GetValue();
        });
        return app;
    }
}