﻿using Newtonsoft.Json;
using PolarisContacts.Domain;
using System.Text;


public class EmailControllerTests : IClassFixture<IntegrationTestFixture>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture _fixture;

    public EmailControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task UpdateEmail_ReturnSuccess()
    {
        var email = new Email
        {
            Id = 1,
            IdContato = 2,
            EnderecoEmail = "Jhonatanpsilva77@gmail.com",
            Ativo = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

        // Faz a requisição para inserir o contato
        var response = await _client.PostAsync("/Email/UpdateEmail", content);
        var responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Alterado com sucesso!", (string)responseObject.message);
    }

    [Fact]
    public async Task DeleteEmail_ReturnsSuccess()
    {
        // Insere um contato de teste
        var emailId = 1;

        // Formata a URL da requisição
        var requestUri = $"/Email/DeleteEmail?id={emailId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

        var response = await _client.SendAsync(request);

        // Verifica se a resposta é bem-sucedida
        Assert.True(response.IsSuccessStatusCode);
    }

}
