namespace Infrastructure.Cqrs.Models.Commands;

public interface ICommandHandler<in TCommand, TCommandResult>
    where TCommand : ICommand
{
    Task<TCommandResult> HandleAsync(TCommand command);
}