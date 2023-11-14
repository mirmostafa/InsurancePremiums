using Service.Infrastructure.Cqrs.Models.Commands;

namespace Service.Domain.Entities;

public sealed class DeleteInvestmentRequestCommand(Guid requestId) : ICommand
{
    public Guid RequestId { get; } = requestId;
}