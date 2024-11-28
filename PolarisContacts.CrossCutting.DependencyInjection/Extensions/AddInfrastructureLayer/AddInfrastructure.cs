using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;
using PolarisContacts.Infrastructure.Repositories;

namespace PolarisContacts.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;

public static partial class AddInfrastructureLayerExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services) =>
        services.AddBindedSettings<UrlApis>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<IContatoRepository, ContatoRepository>()
                .AddScoped<ITelefoneRepository, TelefoneRepository>()
                .AddScoped<ICelularRepository, CelularRepository>()
                .AddScoped<IEmailRepository, EmailRepository>()
                .AddScoped<IRegiaoRepository, RegiaoRepository>()
                .AddScoped<IEnderecoRepository, EnderecoRepository>();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSettings()
            .AddRepositories();
}