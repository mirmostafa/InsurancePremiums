using Infrastructure.Bcl.Results;

using Service.Domain.Entities;

namespace Service.Domain.Dtos;

public sealed class GetAllCoveragesQueryResult(Result<IEnumerable<CoverageDto>> result)
{
    public Result<IEnumerable<CoverageDto>> Result { get; } = result;
}