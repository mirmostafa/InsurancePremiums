using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetCoverageByNameQuery(string name) : IQuery<GetCoverageByNameQueryResult>
{
    public string Name { get; } = name.ArgumentNotNull();
}