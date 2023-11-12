using Infrastructure.Bcl.Results;

namespace Service.Domain.Dtos;

public sealed class GetAllCoveragesQueryResult(Result<IEnumerable<CoverageDto>> coverages)
{
    public Result<IEnumerable<CoverageDto>> Coverages { get; } = coverages;
}