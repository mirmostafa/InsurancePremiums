using Infrastructure.Bcl.Helpers;

using Service.Application.Internals;
using Service.Domain.Dtos;
using Service.Domain.Entities;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Application.Queries;

internal sealed class CalculateInsuranceCostQueryHandler(IQueryProcessor queryProcessor) : IQueryHandler<CalculateInsuranceCostQuery, CalculateInsuranceCostQueryResult>
{
    public async Task<CalculateInsuranceCostQueryResult> HandleAsync(CalculateInsuranceCostQuery query)
    {
        var requestQueryResult = await queryProcessor.ExecuteAsync(new GetInvestmentRequestByIdQuery(query.RequestId));
        if (requestQueryResult.Result.IsFailure)
        {
            return new(requestQueryResult.Result);
        }
        var request = requestQueryResult.Result.GetValue()!;
        foreach(var value in request.Values)
        {
            var coverageId = value.CoverageId;
            var rateQueryResult = await queryProcessor.ExecuteAsync(new GetCoverageRateByCoverageId(coverageId));
            if (rateQueryResult.Result.IsFailure)
            {

            }
            var rate = rateQueryResult.Result.GetValue()!;
            var cost = Calculator.CalculateInsuranceCost()
        }
    }
}
