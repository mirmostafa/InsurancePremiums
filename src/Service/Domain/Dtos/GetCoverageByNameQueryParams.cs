using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Dtos;

public sealed class GetCoverageByNameQueryParams(string name) : IQuery<GetCoverageByNameQueryResult>
{
    public string Name { get; } = name.ArgumentNotNull();
}