using Autofac;

using Microsoft.AspNetCore.Mvc;

using Service.Domain.Entities;
using Service.Domain.ValueObjects;
using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Cqrs.Models.Commands;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Api.Controllers;

public static class CoverageAPIs
{
    public static WebApplication MapApis(WebApplication app, IContainer container)
    {
        _ = app.MapGet("/ip/coverage", async () =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetAllCoveragesQuery());
            return result.Result.ThrowOnFail().GetValue();
        });

        _ = app.MapGet("/ip/coverage/{id:guid}", async (Guid id) =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetCoverageByIdQuery(id));
            return result.Result.ThrowOnFail().GetValue();
        });

        _ = app.MapGet("/ip/coverage/{name}", async (string name) =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new GetCoverageByNameQuery(name));
            return result.Result.ThrowOnFail().GetValue();
        });

        _ = app.MapPost("/ip/coverage/request", async ([FromBody] CoverageInvestRequest request) =>
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

            var cmdParams = new InsertInvestmentRequestCommand(Guid.Empty, request.Title, investments);
            using var scope = container.BeginLifetimeScope();
            var commandProcessor = scope.Resolve<ICommandProcessor>();
            var result = await commandProcessor.ExecuteAsync<InsertInvestmentRequestCommand, InsertInvestmentRequestCommandResult>(cmdParams);
            return result.Result.ThrowOnFail();
        });

        _ = app.MapPut("/ip/coverage/request/{id:guid}", async (Guid id, [FromBody] CoverageInvestRequest request) =>
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

            var cmdParams = new UpdateInvestmentRequestCommand(id, Guid.Empty, request.Title, investments);
            using var scope = container.BeginLifetimeScope();
            var commandProcessor = scope.Resolve<ICommandProcessor>();
            var result = await commandProcessor.ExecuteAsync<UpdateInvestmentRequestCommand, UpdateInvestmentRequestCommandResult>(cmdParams);
            return result.Result.ThrowOnFail();
        });

        _ = app.MapDelete("/ip/coverage/request/{id:guid}", async (Guid id) =>
        {
            var cmdParams = new DeleteInvestmentRequestCommand(id);
            using var scope = container.BeginLifetimeScope();
            var commandProcessor = scope.Resolve<ICommandProcessor>();
            var result = await commandProcessor.ExecuteAsync<DeleteInvestmentRequestCommand, DeleteInvestmentRequestCommandResult>(cmdParams);
            return result.Result.ThrowOnFail();
        });

        _ = app.MapGet("/ip/coverage/request/cost", async (Guid id) =>
        {
            using var scope = container.BeginLifetimeScope();
            var queryProcessor = scope.Resolve<IQueryProcessor>();
            var result = await queryProcessor.ExecuteAsync(new CalculateInsuranceCostQuery(id));
            return result.Result.ThrowOnFail().GetValue();
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