using Service.Application.DataSources;
using Service.Domain.Entities;
using Service.Infrastructure.Cqrs.Models.Commands;

namespace Service.Application.Commands;

internal sealed class UpdateInvestmentRequestCommandHandler(ICommandProcessor commandProcessor, InsurancePremiumsWriteDbContext dbContext, CancellationToken cancellationToken = default) : ICommandHandler<UpdateInvestmentRequestCommand, UpdateInvestmentRequestCommandResult>
{
    public async Task<UpdateInvestmentRequestCommandResult> HandleAsync(UpdateInvestmentRequestCommand command)
    {
        // Delete ths old one.
        var deleteCommandResult = await commandProcessor.ExecuteAsync<DeleteInvestmentRequestCommand, DeleteInvestmentRequestCommandResult>(new DeleteInvestmentRequestCommand(command.RequestId));
        if (deleteCommandResult.Result.IsFailure)
        {
            return new(deleteCommandResult.Result);
        }

        // Insert a new one.
        var insertParams = new InsertInvestmentRequestCommand(command.UserId, command.Title, command.Investments);
        var insertCommandResult = await commandProcessor.ExecuteAsync<InsertInvestmentRequestCommand, InsertInvestmentRequestCommandResult>(insertParams);
        if (insertCommandResult.Result.IsFailure)
        {
            return new(insertCommandResult.Result);
        };
        return new(Infrastructure.Bcl.Results.Result.Success);
    }
}