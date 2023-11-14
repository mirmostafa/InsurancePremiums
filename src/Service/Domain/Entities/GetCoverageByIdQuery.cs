using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetCoverageByIdQuery(Guid id) : IQuery<GetCoverageByIdQueryResult>
{
    public Guid Id { get; } = id.ArgumentNotNull();
}