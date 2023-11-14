using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Application.Internals;
using Service.Domain.Dtos;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Queries;
using Service.Infrastructure.Exceptions;

namespace Service.Application.Queries;

internal sealed class GetInvestmentRequestByIdQueryHandler(InsurancePremiumsReadDbContext readDbContext, CancellationToken cancellationToken = default) : IQueryHandler<GetCoverageByIdQuery, GetCoverageByIdQueryResult>
{
    public async Task<GetCoverageByIdQueryResult> HandleAsync(GetCoverageByIdQuery query)
    {
        var dbQuery = from coverage in readDbContext.Coverages
                      where coverage.Id == query.Id
                      select coverage;
        var dbResult = await dbQuery.FirstOrDefaultAsync(cancellationToken);
        if (dbResult == null)
        {
            return new(Result<CoverageDto>.CreateFailure(new NoItemFoundException("No request found with the specific Id.")));
        }
        var result = dbResult.ToDto();
        return new(Result.CreateSuccess(result)!);
    }
}