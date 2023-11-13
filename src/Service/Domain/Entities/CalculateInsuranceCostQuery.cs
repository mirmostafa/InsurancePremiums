using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class CalculateInsuranceCostQuery : IQuery<CalculateInsuranceCostQueryResult>
{
    public Guid RequestId { get; }
}
