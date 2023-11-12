using Autofac;

using Infrastructure.Bcl.Validations;
using Infrastructure.Cqrs.Models.Commands;

namespace Infrastructure.Cqrs.Engine.Command;

internal sealed class CommandProcessor : ICommandProcessor
{
    private readonly ILifetimeScope _container;

    public CommandProcessor(ILifetimeScope container) =>
        this._container = container;

#if !DEBUG
        [System.Diagnostics.DebuggerStepThrough]
#endif
    public Task<TCommandResult> ExecuteAsync<Parameters, TCommandResult>(Parameters parameters)
    {
        Checker.MustBeArgumentNotNull(parameters);

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(parameters.GetType(), typeof(TCommandResult));
        dynamic handler = this._container.ResolveKeyed("2", handlerType);
        return handler.HandleAsync((dynamic)parameters);
    }
}