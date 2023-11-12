using Infrastructure.Bcl.Results;
using Infrastructure.Cqrs.Models.Queries;

namespace Service.Domain.Dtos;

public sealed class GetAllCoveragesQueryParams : IQuery<GetAllCoveragesQueryResult>
{
}