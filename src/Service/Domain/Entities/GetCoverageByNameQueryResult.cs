using Service.Domain.Dtos;
using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public class GetCoverageByNameQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}