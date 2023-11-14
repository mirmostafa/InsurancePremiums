using Service.Domain.ValueObjects;
using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class GetCoverageRateByCoverageIdResult(Result<Rate> result)
{
    public Result<Rate> Result { get; } = result;
}