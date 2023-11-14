using Service.Domain.Dtos;
using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public class GetCoverageByIdQueryResult(Result<CoverageDto?> result)
{
    public Result<CoverageDto?> Result { get; } = result;
}