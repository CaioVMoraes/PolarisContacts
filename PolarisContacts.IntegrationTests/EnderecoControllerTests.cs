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
    public async Task UpdateEndereco_ReturnSuccess()
    {
        var endereco = new Endereco
        {
            Id = 1,
            IdContato = 2,
            Logradouro = "Rua dos Cabiros",
            Numero = "36",
            Cidade = "São Paulo",
            Estado = "SP",
            Bairro = "Jardim Itajaí",
            Complemento = "Casa",
            CEP = "04855-140",
            Ativo = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(endereco), Encoding.UTF8, "application/json");

        // Faz a requisição para inserir o contato
        var response = await _client.PostAsync("/Endereco/UpdateEndereco", content);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Alterado com sucesso!", (string)responseObject.message);
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
