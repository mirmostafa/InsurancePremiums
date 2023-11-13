using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Commands;

using Service.Domain.ValueObjects;

namespace Service.Domain.Entities;

public sealed class InsertInvestOnCoverageCommandParams(Guid userId, string title, CoverageInvestments investments) : ICommand
{
    public CoverageInvestments Investments { get; } = investments;
    public Guid UserId { get; } = userId;
    public string Title { get; } = title;
}