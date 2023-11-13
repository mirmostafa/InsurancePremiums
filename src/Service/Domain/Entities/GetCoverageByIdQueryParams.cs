using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetCoverageByIdQueryParams(Guid id) : IQuery<GetCoverageByIdQueryResult>
{
    public Guid Id { get; } = id.ArgumentNotNull();
}