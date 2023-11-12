using Infrastructure.Bcl.Results;

using Service.Domain.Entities;

namespace Service.Domain.Dtos;

public class GetCoverageByNameQueryResult
{
    public GetCoverageByNameQueryResult(Result<CoverageDto?> result)
    {
        this.Result = result;
    }

    public Result<CoverageDto?> Result { get; }
}