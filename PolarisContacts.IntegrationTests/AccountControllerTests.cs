using Newtonsoft.Json;
using PolarisContacts.Domain;
using System.Text;


public class AccountControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public AccountControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task ChangePassword_ReturnSuccess()
    {
        // Define os dados de teste
        string login = "Login Teste";
        string oldPassword = "1";
        string newPassword = "newPassword";
        string confirmNewPassword = "newPassword";

        // Cria a URL com os parâmetros na query string
        var url = $"Account/ChangePassword?login={login}&oldPassword={oldPassword}&newPassword={newPassword}&confirmNewPassword={confirmNewPassword}";

        // Faz a requisição para alterar a senha via POST
        var response = await _client.PostAsync(url, null); // Passa null no conteúdo, pois os dados estão na query string

        // Lê o conteúdo da resposta
        var responseContent = await response.Content.ReadAsStringAsync();

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }
}
