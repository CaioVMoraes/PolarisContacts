using Newtonsoft.Json;
using PolarisContacts.Domain;
using System.Text;


public class EnderecoControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public EnderecoControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }   

    [Fact]
    public async Task DeleteEndereco_ReturnsSuccess()
    {
        // Insere um contato de teste
        var enderecoId = 1;

        // Formata a URL da requisição
        var requestUri = $"/Endereco/DeleteEndereco?id={enderecoId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _client.SendAsync(request);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }
}
