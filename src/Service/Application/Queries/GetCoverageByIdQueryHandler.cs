using Microsoft.EntityFrameworkCore;

using Service.Application.DataSources;
using Service.Application.Internals;
using Service.Domain.Dtos;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Queries;
using Service.Infrastructure.Exceptions;

namespace Service.Application.Queries;

internal sealed class GetCoverageByIdQueryHandler(InsurancePremiumsReadDbContext readDbContext, CancellationToken cancellationToken = default) : IQueryHandler<GetCoverageByIdQuery, GetCoverageByIdQueryResult>
{
    public async Task<GetCoverageByIdQueryResult> HandleAsync(GetCoverageByIdQuery query)
    {
        Result<CoverageDto?> result;
        var dbQuery = from coverage in readDbContext.Coverages
                      where coverage.Id == query.Id
                      select coverage;
        var dbResult = await dbQuery.FirstOrDefaultAsync(cancellationToken);
        if (dbResult == null)
        {
            result = Result<CoverageDto>.CreateFailure(new NoItemFoundException("No coverage found with the specific Id."));
        }
        var dto = dbResult.ToDto();
        result = Result.CreateSuccess(dto);
        return new(result);
    }
}