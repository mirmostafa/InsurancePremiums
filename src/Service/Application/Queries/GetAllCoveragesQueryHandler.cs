using Infrastructure.Cqrs.Models.Queries;

using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Application.Internals;
using Service.Domain.Dtos;
using Service.Infrastructure.Bcl.Results;

namespace Service.Application.Queries;

internal sealed class GetAllCoveragesQueryHandler(InsurancePremiumsReadDbContext readDbContext, CancellationToken cancellationToken = default) : IQueryHandler<GetAllCoveragesQueryParams, GetAllCoveragesQueryResult>
{
    public async Task<GetAllCoveragesQueryResult> HandleAsync(GetAllCoveragesQueryParams query)
    {
        var dbQuery = from coverage in readDbContext.Coverages
                      select coverage;
        var dbResult = await dbQuery.ToListAsync(cancellationToken);
        var result = dbResult.ToDto();
        return new(Result.CreateSuccess(result));
    }
}