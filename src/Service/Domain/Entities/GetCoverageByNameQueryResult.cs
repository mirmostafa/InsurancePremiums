using Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public class GetCoverageByNameQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}

public class GetCoverageByIdQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}