using Newtonsoft.Json;
using PolarisContacts.Domain;
using System.Text;


public class TelefoneControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public TelefoneControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task UpdateTelefone_ReturnSuccess()
    {
        var telefone = new Telefone
        {
            Id = 1,
            IdRegiao = 1,
            IdContato = 2,
            NumeroTelefone = "5526-9799",
            Ativo = true,
            Regiao = new Regiao
            {
                Id = 1,
                DDD = "021",
                NomeRegiao = "Rio de Janeiro",
                Ativo = true
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(telefone), Encoding.UTF8, "application/json");

        // Faz a requisição para inserir o contato
        var response = await _client.PostAsync("/Telefone/UpdateTelefone", content);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Alterado com sucesso!", (string)responseObject.message);
    }

    [Fact]
    public async Task DeleteTelefone_ReturnsSuccess()
    {
        // Insere um contato de teste
        var TelefoneId = 1;

        // Formata a URL da requisição
        var requestUri = $"/Telefone/DeleteTelefone?id={TelefoneId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _client.SendAsync(request);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }

}
