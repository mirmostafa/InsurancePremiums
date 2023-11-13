using Service.Domain.ValueObjects;
using Service.Infrastructure.Cqrs.Models.Commands;

namespace Service.Domain.Entities;

public sealed class UpdateInvestmentRequestCommand(Guid requestId, Guid userId, string title, CoverageInvestments investments) : ICommand
{
    public CoverageInvestments Investments { get; } = investments;
    public Guid RequestId { get; } = requestId;
    public string Title { get; } = title;
    public Guid UserId { get; } = userId;
}