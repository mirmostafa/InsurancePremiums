using Infrastructure.Bcl.Results;

using Service.Domain.Dtos;

namespace Service.Domain.Entities;

public sealed class GetAllCoveragesQueryResult(Result<IEnumerable<CoverageDto>> result)
{
    public Result<IEnumerable<CoverageDto>> Result { get; } = result;
}