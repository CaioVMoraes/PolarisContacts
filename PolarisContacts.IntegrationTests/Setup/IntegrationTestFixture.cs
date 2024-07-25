using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

public class IntegrationTestFixture : WebApplicationFactory<Program> // Use Program como tipo base
{
    public HttpClient Client { get; private set; }

    public IntegrationTestFixture()
    {
        // Configura o ambiente de teste
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        Client = CreateClient();
    }

    [CollectionDefinition("Integration")]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestFixture>
    {
        // Esta classe não contém código, serve apenas para definir a coleção
    }
}
