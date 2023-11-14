using Service.Domain.Dtos;
using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class GetAllCoveragesQueryResult(Result<IEnumerable<CoverageDto>> result)
{
    public Result<IEnumerable<CoverageDto>> Result { get; } = result;
}