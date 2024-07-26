using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PolarisContacts.Domain;
using Xunit;

public class HomeControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public HomeControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task InsertContato_ReturnsSuccess()
    {
        // Cria um contato de teste
        var contato = new Contato
        {
            Nome = "Contato de Teste",
            IdUsuario = 2, // Use um ID de usuário válido
            Ativo = true,
            Celulares = new List<Celular>
            {
                new Celular
                {
                    IdRegiao = 1, // Use um ID de região válido
                    NumeroCelular = "12345-6789",
                    Ativo = true
                }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(contato), Encoding.UTF8, "application/json");

        // Faz a requisição para inserir o contato
        var response = await _client.PostAsync("/Home/InsertContato", content);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task DeleteContato_ReturnsSuccess()
    {
        // Insere um contato de teste
        var contatoId = 1;

        // Formata a URL da requisição
        var requestUri = $"/Home/DeleteContato?id={contatoId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _client.SendAsync(request);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }

}
