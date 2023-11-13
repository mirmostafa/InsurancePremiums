using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class GetCoverageRateByCoverageIdResult(Result<decimal> result)
{
    public Result<decimal> Result { get; } = result;
}