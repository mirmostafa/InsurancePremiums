using Service.Application.Internals;
using Service.Domain.Entities;
using Service.Domain.ValueObjects;
using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Application.Queries;

internal sealed class CalculateInsuranceCostQueryHandler(IQueryProcessor queryProcessor) : IQueryHandler<CalculateInsuranceCostQuery, CalculateInsuranceCostQueryResult>
{
    public async Task<CalculateInsuranceCostQueryResult> HandleAsync(CalculateInsuranceCostQuery query)
    {
        var requestQueryResult = await queryProcessor.ExecuteAsync(new GetInvestmentRequestByIdQuery(Guid.Empty, query.RequestId));
        if (requestQueryResult.Result.IsFailure)
        {
            return new(requestQueryResult.Result.WithValue(Enumerable.Empty<(Guid CoverageId, string CoverageName, decimal Cost)>()));
        }
        var request = requestQueryResult.Result.GetValue()!;
        var result = new List<(Guid CoverageId, string CoverageName, decimal Cost)>();
        var coverageRateCache = new Dictionary<Guid, Rate>();
        foreach (var value in request.Values)
        {
            if (!coverageRateCache.TryGetValue(value.CoverageId, out var rate))
            {
                var rateQueryResult = await queryProcessor.ExecuteAsync(new GetCoverageRateByCoverageId(value.CoverageId));
                if (rateQueryResult.Result.IsFailure)
                {
                    return new(rateQueryResult.Result.WithValue(Enumerable.Empty<(Guid CoverageId, string CoverageName, decimal Cost)>()));
                }

                rate = rateQueryResult.Result.GetValue()!;
                coverageRateCache.Add(value.CoverageId, rate);
            }

            var cost = Calculator.CalculateInsuranceCost(rate.ToPrimitiveType(), value.Value);
            (Guid CoverageId, string CoverageName, decimal Cost) item = (value.CoverageId, "", cost);
            result.Add(item);
        }
        return new(Result.CreateSuccess(result.AsEnumerable()));
    }
}