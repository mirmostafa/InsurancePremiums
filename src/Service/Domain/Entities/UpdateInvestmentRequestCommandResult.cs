using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class UpdateInvestmentRequestCommandResult(Result result)
{
    public Result Result { get; } = result;
}