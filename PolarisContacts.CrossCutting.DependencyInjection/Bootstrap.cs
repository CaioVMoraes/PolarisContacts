using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddApplicationLayer;
using PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;

namespace PolarisContacts.CrossCutting.DependencyInjection;

public static class Bootstrap
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .AddInfrastructure()
            .AddApplication();
}
