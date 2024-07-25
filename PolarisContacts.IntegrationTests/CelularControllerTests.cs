using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

[Collection("Integration")]
public class CelularControllerTests
{
    private readonly IntegrationTestFixture _fixture;

    public CelularControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteCelular_ReturnsSuccess()
    {
        // Arrange
        var id = 1; // Substitua com um ID válido para o teste
        var requestUri = $"/Celular/DeleteCelular?id={id}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _fixture.Client.SendAsync(request);

        var responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
        response.EnsureSuccessStatusCode();

        try
        {
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseBody);
            Assert.NotNull(responseJson);
            Assert.Equal(true, (bool)responseJson.success);
        }
        catch (JsonReaderException)
        {
            Assert.True(false, "A resposta não é JSON. Conteúdo retornado: " + responseBody);
        }
    }
}
