using Service.Application.DataSources;
using Service.Domain.Entities;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Cqrs.Models.Commands;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Application.Commands;

internal sealed class DeleteInvestmentRequestCommandHandler(IQueryProcessor queryProcessor, InsurancePremiumsWriteDbContext dbContext, CancellationToken cancellationToken = default) : ICommandHandler<DeleteInvestmentRequestCommand, DeleteInvestmentRequestCommandResult>
{
    public async Task<DeleteInvestmentRequestCommandResult> HandleAsync(DeleteInvestmentRequestCommand command)
    {
        var requestQueryResult = await queryProcessor.ExecuteAsync(new GetInvestmentRequestByIdQuery(command.RequestId));
        if (requestQueryResult.Result.IsFailure)
        {
            return new(requestQueryResult.Result);
        }
        var entity = new InvestmentRequest { Id = command.RequestId };

        _ = dbContext.InvestmentRequests.Remove(entity);
        _ = await dbContext.SaveChangesAsync();
        return new(Result.Success);
    }
}