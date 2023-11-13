using Service.Infrastructure.Bcl.Results;

namespace Service.Domain.Entities;

public sealed class DeleteInvestmentRequestCommandResult(Result result)
{
    public Result Result { get; } = result;
}