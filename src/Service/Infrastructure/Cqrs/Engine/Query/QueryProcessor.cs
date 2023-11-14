using Autofac;

using Service.Infrastructure.Cqrs.Models.Queries;

namespace Service.Infrastructure.Cqrs.Engine.Query;

internal sealed class QueryProcessor : IQueryProcessor
{
    private readonly ILifetimeScope _container;

    public QueryProcessor(ILifetimeScope container) =>
        this._container = container;

    [System.Diagnostics.DebuggerStepThrough]
    public Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
    {
        Checker.MustBeArgumentNotNull(query);

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        dynamic handler = this._container.ResolveKeyed("1", handlerType);
        return handler.HandleAsync((dynamic)query);
    }
}