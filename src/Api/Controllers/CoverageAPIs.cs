using Autofac;

using Infrastructure.Bcl.Helpers;
using Infrastructure.Cqrs.Models.Commands;
using Infrastructure.Cqrs.Models.Queries;

using Microsoft.AspNetCore.Mvc;

using Service.Domain.Entities;
using Service.Domain.ValueObjects;

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
        //_ = app.MapPost("/ip/coverage/request", (HttpRequest request) =>
        //{
        //    var requestBody = string.Empty;
        //    using (var reader = new StreamReader(request.Body))
        //    {
        //        requestBody = reader.ReadToEnd();
        //    }
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };

        //    var investRequest = JsonSerializer.Deserialize<CoverageInvestRequest>(requestBody, options);
        //});
        _ = app.MapPost("/ip/coverage/request",
        async ([FromBody] CoverageInvestRequest request) =>
        {
            var investments = new CoverageInvestments();
            investments.Investments[0] = new(request.Coverage1Id, request.Value1);
            if (request.Coverage2Id != null && request.Value2 != null)
            {
                investments.Investments[1] = new(request.Coverage2Id.Value, request.Value2.Value);
            }
            if (request.Coverage3Id != null && request.Value3 != null)
            {
                investments.Investments[2] = new(request.Coverage3Id.Value, request.Value3.Value);
            }

            var cmdParams = new InsertInvestOnCoverageCommandParams(Guid.Empty, request.Title, investments);
            using var scope = container.BeginLifetimeScope();
            var commandProcessor = scope.Resolve<ICommandProcessor>();
            var result = await commandProcessor.ExecuteAsync<InsertInvestOnCoverageCommandParams, InsertInvestOnCoverageCommandResult>(cmdParams);
            return result.Result;
        });
        return app;
    }
}

public sealed class CoverageInvestRequest
{
    public Guid Coverage1Id { get; set; }
    public Guid? Coverage2Id { get; set; }
    public Guid? Coverage3Id { get; set; }
    public string Title { get; set; }
    public decimal Value1 { get; set; }
    public decimal? Value2 { get; set; }
    public decimal? Value3 { get; set; }
}