using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddApplicationLayer;

public static partial class AddApplicationLayerExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<ITelefoneService, TelefoneService>()
                .AddTransient<IContatoService, ContatoService>()
                .AddTransient<IEnderecoService, EnderecoService>();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddServices();
}
