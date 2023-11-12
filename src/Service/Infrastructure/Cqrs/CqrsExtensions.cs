using System.Reflection;

using Autofac;

using Infrastructure.Cqrs.Engine.Command;
using Infrastructure.Cqrs.Models.Commands;
using Infrastructure.Cqrs.Models.Queries;

using Service.Infrastructure.Cqrs.Engine.Query;

namespace Service.Infrastructure.Cqrs;

public static class CqrsExtensions
{
    public static ContainerBuilder AddCqrs(this ContainerBuilder builder, params Assembly[] scannedAssemblies)
    {
        _ = builder.RegisterType<QueryProcessor>()
                .As<IQueryProcessor>()
                .InstancePerLifetimeScope();

        _ = builder
                .RegisterAssemblyTypes(scannedAssemblies)
                .AsClosedTypesOf(typeof(IQueryHandler<,>), "1")
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        _ = builder
                .RegisterAssemblyTypes(scannedAssemblies)
                .AsClosedTypesOf(typeof(ICommandHandler<,>), "2")
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        _ = builder
                .RegisterAssemblyTypes(scannedAssemblies)
                .AsClosedTypesOf(typeof(ICommandValidator<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        _ = builder
                .RegisterType<CommandProcessor>()
                .As<ICommandProcessor>()
                .InstancePerLifetimeScope();

        _ = builder
                .RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<,>),
                    typeof(ICommandHandler<,>),
                    "1",
                    "2")
                .InstancePerLifetimeScope();

        return builder;
    }
}