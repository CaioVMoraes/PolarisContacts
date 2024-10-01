using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddApplicationLayer;

public static partial class AddApplicationLayerExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddScoped<IUsuarioService, UsuarioService>()
                .AddScoped<IContatoService, ContatoService>()
                .AddScoped<ITelefoneService, TelefoneService>()
                .AddScoped<ICelularService, CelularService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IRegiaoService, RegiaoService>()
                .AddScoped<IEnderecoService, EnderecoService>();

    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddServices();
}
