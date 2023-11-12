using Infrastructure.Bcl.Results;
using Infrastructure.Cqrs.Models.Queries;

using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Application.Internals;
using Service.Domain.Dtos;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Exceptions;

namespace Service.Application.Queries;

internal sealed class GetCoverageByNameQueryHandler(InsurancePremiumsReadDbContext readDbContext, CancellationToken cancellationToken = default) : IQueryHandler<GetCoverageByNameQueryParams, GetCoverageByNameQueryResult>
{
    public async Task<GetCoverageByNameQueryResult> HandleAsync(GetCoverageByNameQueryParams query)
    {
        var dbQuery = from coverage in readDbContext.Coverages
                      where coverage.Name == query.Name
                      select coverage;
        var dbResult = await dbQuery.FirstOrDefaultAsync(cancellationToken);
        if (dbResult == null)
        {
            return new(Result<CoverageDto>.CreateFailure(new NoItemFoundException("No coverage found with specific name.")));
        }
        var result = dbResult.ToDto();
        return new(Result.CreateSuccess(result));
    }
}