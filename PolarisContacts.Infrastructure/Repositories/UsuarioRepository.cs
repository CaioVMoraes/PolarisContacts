using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using PolarisContacts.Domain.Settings;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class UsuarioRepository(IOptions<UrlApis> urlApis) : IUsuarioRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<Usuario> GetUserByPasswordAsync(string login, string senha)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            string url = $"{_urlApis.ReadService}/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter usuário: {response.StatusCode}");
            }
        }

        public async Task<bool> CreateUserAsync(Usuario usuario)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.CreateService}/Usuario/CreateUser/", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPasswordAsync(string login, string oldPassword, string newPassword)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(new Usuario { Login = login, Senha = oldPassword, NovaSenha = newPassword });
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Usuario/ChangeUserPasswordAsync/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao alterar a senha!");
            }
        }
    }
}
