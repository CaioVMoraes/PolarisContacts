using Newtonsoft.Json;
using PolarisContacts.Domain;
using System.Text;


public class CelularControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public CelularControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task UpdateCelular_ReturnSuccess()
    {
        var celular = new Celular
        {
            Id = 1,
            IdRegiao = 1,
            IdContato = 2,
            NumeroCelular = "99589-8478",
            Ativo = true,
            Regiao = new Regiao
            {
                Id = 1,
                DDD = "011",
                NomeRegiao = "São Paulo",
                Ativo = true
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(celular), Encoding.UTF8, "application/json");

        // Faz a requisição para inserir o contato
        var response = await _client.PostAsync("/Celular/UpdateCelular", content);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Alterado com sucesso!", (string)responseObject.message);
    }

    [Fact]
    public async Task DeleteCelular_ReturnsSuccess()
    {
        // Insere um contato de teste
        var celularId = 1;

        // Formata a URL da requisição
        var requestUri = $"/Celular/DeleteCelular?id={celularId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _client.SendAsync(request);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }

}
