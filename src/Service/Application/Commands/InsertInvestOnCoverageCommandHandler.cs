using Infrastructure.Bcl.Helpers;
using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Commands;
using Infrastructure.Cqrs.Models.Queries;

using Service.Application.DataSources;
using Service.Domain.Entities;
using Service.Domain.ValueObjects;
using Service.Infrastructure.Bcl.Results;

namespace Service.Application.Commands;

internal sealed class InsertInvestOnCoverageCommandHandler(IQueryProcessor queryProcessor, InsurancePremiumsWriteDbContext dbContext, CancellationToken cancellationToken = default) : ICommandHandler<InsertInvestOnCoverageCommandParams, InsertInvestOnCoverageCommandResult>
{
    public async Task<InsertInvestOnCoverageCommandResult> HandleAsync(InsertInvestOnCoverageCommandParams command)
    {
        await validate();
        var request = await createRequest();
        await createValuesOnRequest(request);
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
        async Task createValuesOnRequest(InvestmentRequest request)
        {
            var data = new List<(Guid coverageId, decimal value)>();
            foreach (var investment in command.Investments)
            {
                data.Add(await processCoverageInvestmentAsync(investment));
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

            async Task<(Guid CoverageId, decimal Investment)> processCoverageInvestmentAsync(CoverageInvestment investment)
            {
                if (investment == CoverageInvestment.Default || investment is null)
                {
                    return default;
                }
                (var coverageId, var value) = investment;
                var coverageQuery = await queryProcessor.ExecuteAsync(new GetCoverageByIdQueryParams(coverageId));
                var coverage = coverageQuery.Result.ThrowOnFail().GetValue()!;
                Check.MustBe(value >= coverage.InvestmentMin && value <= coverage.InvestmentMax, () => "Investment value is out of range");
                return (coverageId, value);
            }
        }
        async Task<Result> saveResult()
        {
            _ = await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
    }
}