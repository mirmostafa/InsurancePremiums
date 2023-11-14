using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class InsertInvestmentRequestCommandResult(Result result)
{
    public Result Result { get; } = result;
}