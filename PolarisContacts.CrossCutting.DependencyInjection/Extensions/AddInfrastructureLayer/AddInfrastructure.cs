using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;
using PolarisContacts.Infrastructure.Repositories;

namespace Job.ReguaCobrancaDocumentos.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;

public static partial class AddInfrastructureLayerExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services) =>
        services.AddBindedSettings<DbSettings>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddTransient<IUsuarioRepository, UsuarioRepository>()
                .AddTransient<IContatoRepository, ContatoRepository>()
                .AddTransient<ITelefoneRepository, TelefoneRepository>()
                .AddTransient<ICelularRepository, CelularRepository>()
                .AddTransient<IEmailRepository, EmailRepository>()
                .AddTransient<IRegiaoRepository, RegiaoRepository>()
                .AddTransient<IEnderecoRepository, EnderecoRepository>()
                .AddTransient<IDatabaseConnection, DatabaseConnection>();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSettings()
            .AddRepositories();
}