using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class CalculateInsuranceCostQueryResult(Result<IEnumerable<(Guid CoverageId, string CoverageName, decimal Cost)>> result)
{
    public Result<IEnumerable<(Guid CoverageId, string CoverageName, decimal Cost)>> Result { get; } = result;
}