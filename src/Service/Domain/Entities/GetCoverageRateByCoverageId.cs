using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetCoverageRateByCoverageId(Guid coverageId) : IQuery<GetCoverageRateByCoverageIdResult>
{
    public Guid CoverageId { get; } = coverageId;
}
