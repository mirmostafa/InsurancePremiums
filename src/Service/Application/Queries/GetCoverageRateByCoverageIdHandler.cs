using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Queries;
using Service.Infrastructure.Exceptions;

namespace Service.Application.Queries;

internal sealed class GetCoverageRateByCoverageIdHandler(InsurancePremiumsReadDbContext dbContext) : IQueryHandler<GetCoverageRateByCoverageId, GetCoverageRateByCoverageIdResult>
{
    public async Task<GetCoverageRateByCoverageIdResult> HandleAsync(GetCoverageRateByCoverageId query)
    {
        Result<decimal> result;
        var dbQuery = from x in dbContext.Coverages
                      where x.Id == query.CoverageId
                      select x.Rate;
        var dbResult = await dbQuery.FirstOrDefaultAsync();
        if (dbResult == 0)
        {
            result = Result<decimal>.CreateFailure(new NoItemFoundException("No coverage found with the specific Id."));
        }
        result = Result.CreateSuccess(dbResult);
        return new(result);
    }
}