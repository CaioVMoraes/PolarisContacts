using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Infrastructure.Repositories;

namespace Job.ReguaCobrancaDocumentos.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;

public static partial class AddInfrastructureLayerExtensions
{
    //public static IServiceCollection AddSettings(this IServiceCollection services) =>
    //    services.AddBindedSettings<JobsSettings>()
    //            .AddBindedSettings<PaginationSettings>()
    //            .AddBindedSettings<EmailSettings>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddTransient<IPessoaRepository, PessoaRepository>()
                .AddTransient<IContatoRepository, ContatoRepository>()
                .AddTransient<IEnderecoRepository, EnderecoRepository>();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            //.AddSettings()
            .AddRepositories();
}
