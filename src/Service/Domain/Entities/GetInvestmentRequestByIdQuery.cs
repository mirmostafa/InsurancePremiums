using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetInvestmentRequestByIdQuery(Guid id) : IQuery<GetInvestmentRequestByIdQueryResult>
{
    public Guid Id { get; } = id.ArgumentNotNull();
}