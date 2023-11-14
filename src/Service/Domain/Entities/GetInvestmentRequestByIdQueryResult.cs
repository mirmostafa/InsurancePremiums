using Service.Domain.Dtos;
using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public class GetInvestmentRequestByIdQueryResult(Result<InvestmentRequestDto?> result)
{
    public Result<InvestmentRequestDto?> Result { get; } = result;
}