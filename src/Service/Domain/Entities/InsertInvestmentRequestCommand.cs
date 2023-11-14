using Service.Domain.ValueObjects;
using Service.Infrastructure.Cqrs.Models.Commands;

namespace Service.Domain.Entities;

public sealed class InsertInvestmentRequestCommand(Guid userId, string title, CoverageInvestments investments) : ICommand
{
    public CoverageInvestments Investments { get; } = investments;
    public string Title { get; } = title;
    public Guid UserId { get; } = userId;
}