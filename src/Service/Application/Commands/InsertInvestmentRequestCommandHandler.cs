using Service.Application.DataSources;
using Service.Domain.Entities;
using Service.Domain.ValueObjects;
using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Cqrs.Models.Commands;
using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Application.Commands;

internal sealed class InsertInvestmentRequestCommandHandler(IQueryProcessor queryProcessor, InsurancePremiumsWriteDbContext dbContext, CancellationToken cancellationToken = default) : ICommandHandler<InsertInvestmentRequestCommand, InsertInvestmentRequestCommandResult>
{
    public async Task<InsertInvestmentRequestCommandResult> HandleAsync(InsertInvestmentRequestCommand command)
    {
        await validate();
        var request = await createRequest();
        var r1 = await createValuesOnRequest(request);
        if (r1.IsFailure)
        {
            return new(r1);
        }
        var result = await saveResult();
        return new(result);

        Task validate()
        {
            Check.MustBeArgumentNotNull(command);
            Check.MustBeArgumentNotNull(command.Title);
            Check.MustBe(command.Investments.IsValid(), () => "Invalid investment data");
            return Task.CompletedTask;
        }
        Task<InvestmentRequest> createRequest()
        {
            var request = new InvestmentRequest
            {
                CreateDate = DateTime.UtcNow,
                Title = command.Title
            };
            _ = dbContext.InvestmentRequests.Add(request);
            return Task.FromResult(request);
        }
        async Task<Result> createValuesOnRequest(InvestmentRequest request)
        {
            var data = new List<(Guid coverageId, decimal value)>();
            foreach (var investment in command.Investments)
            {
                var item = await processCoverageInvestmentAsync(investment);
                if (item?.IsFailure ?? true)
                {
                    return item ?? Result.Failure;
                }
                data.Add(item);
            }
            foreach (var investment in data)
            {
                var value = new InvestmentValue
                {
                    CoverageId = investment.coverageId,
                    InvestmentRequest = request,
                    Value = investment.value
                };
                _ = dbContext.InvestmentValues.Add(value);
            }
            return Result.Success;

            async Task<Result<(Guid CoverageId, decimal Investment)>?> processCoverageInvestmentAsync(CoverageInvestment investment)
            {
                if (investment == CoverageInvestment.Default || investment is null)
                {
                    return default;
                }
                (var coverageId, var value) = investment;
                var coverageQuery = await queryProcessor.ExecuteAsync(new GetCoverageByIdQuery(coverageId));
                if (coverageQuery.Result.IsFailure)
                {
                    return coverageQuery.Result.WithValue<(Guid, decimal)>(default);
                }
                var coverage = coverageQuery.Result.GetValue()!;
                Check.MustBe(value >= coverage.InvestmentMin && value <= coverage.InvestmentMax, () => "Investment value is out of range");
                return Result.CreateSuccess((coverageId, value));
            }
        }
        async Task<Result> saveResult()
        {
            _ = await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
    }
}