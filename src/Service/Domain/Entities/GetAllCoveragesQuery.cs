using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetAllCoveragesQuery : IQuery<GetAllCoveragesQueryResult>
{
}