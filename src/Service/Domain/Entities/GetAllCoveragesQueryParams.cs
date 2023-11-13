using Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Entities;

public sealed class GetAllCoveragesQueryParams : IQuery<GetAllCoveragesQueryResult>
{
}