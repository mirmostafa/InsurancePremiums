using Autofac;

using Service.Application.DataSources;

namespace Service;

public static class Startup
{
    //public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
    //    services.AddDbContext<InsurancePremiumsWriteDbContext>(options => options.UseSqlServer());

    public static ContainerBuilder AddApplicationServices(this ContainerBuilder builder)
    {
        _ = builder.RegisterType<InsurancePremiumsWriteDbContext>();
        _ = builder.RegisterType<InsurancePremiumsReadDbContext>();
        return builder;
    }
}