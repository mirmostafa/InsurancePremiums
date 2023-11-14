using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetInvestmentRequestByIdQuery(Guid userId, Guid requestId) : IQuery<GetInvestmentRequestByIdQueryResult>
{
    public Guid RequestId { get; } = requestId.ArgumentNotNull();
    public Guid UserId { get; } = userId;
}