using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetCoverageByNameQueryParams(string name) : IQuery<GetCoverageByNameQueryResult>
{
    public string Name { get; } = name.ArgumentNotNull();
}