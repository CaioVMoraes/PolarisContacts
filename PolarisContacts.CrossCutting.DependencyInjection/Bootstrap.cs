using Job.ReguaCobrancaDocumentos.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddApplicationLayer;

namespace PolarisContacts.CrossCutting.DependencyInjection;

public static class Bootstrap
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .AddInfrastructure()
            .AddApplication();
}
