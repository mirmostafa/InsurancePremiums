using Infrastructure.Bcl.Results;

using Service.Domain.Dtos;

namespace Service.Domain.Entities;

public class GetCoverageByNameQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}

public class GetCoverageByIdQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}