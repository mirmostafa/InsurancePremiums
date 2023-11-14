using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Application.Internals;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Application.Queries;

public sealed class GetAllCoveragesQueryHandler(InsurancePremiumsReadDbContext readDbContext, CancellationToken cancellationToken = default) : IQueryHandler<GetAllCoveragesQuery, GetAllCoveragesQueryResult>
{
    public async Task<GetAllCoveragesQueryResult> HandleAsync(GetAllCoveragesQuery query)
    {
        var dbQuery = from coverage in readDbContext.Coverages
                      select coverage;
        var dbResult = await dbQuery.ToListAsync(cancellationToken);
        var result = dbResult.ToDto();
        return new(Result.CreateSuccess(result));
    }
}