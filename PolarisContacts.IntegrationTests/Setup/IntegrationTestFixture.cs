using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL; // Certifique-se de adicionar esta diretiva
using System;
using System.Data;
using System.Net.Http;
using Xunit;

public class IntegrationTestFixture : WebApplicationFactory<Program>
{
    public HttpClient Client { get; private set; }
    private SqliteConnection _connection;

    public IntegrationTestFixture()
    {
        // Configura o ambiente de teste
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        // Configura o WebApplicationFactory e registra o banco de dados em memória
        Client = CreateClient();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            
        });
    }

    [CollectionDefinition("Integration")]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestFixture>
    {
    }
}
