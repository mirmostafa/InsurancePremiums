using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class CalculateInsuranceCostQuery(Guid requestId) : IQuery<CalculateInsuranceCostQueryResult>
{
    public Guid RequestId { get; } = requestId;
}