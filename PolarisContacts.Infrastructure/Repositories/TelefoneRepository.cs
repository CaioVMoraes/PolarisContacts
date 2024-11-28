using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using PolarisContacts.Domain.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class TelefoneRepository(IOptions<UrlApis> urlApis) : ITelefoneRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Telefone/GetTelefonesByIdContato/{idContato}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Telefone>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter telefones: {response.StatusCode}");
            }
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Telefone/GetTelefoneById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Telefone>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter telefones: {response.StatusCode}");
            }
        }

        public async Task<int> AddTelefone(Telefone telefone)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(telefone);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.CreateService}/Telefone/AddTelefone/", content);

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                throw new HttpRequestException($"Erro ao cadastrar o telefone!");
            }
        }

        public async Task<bool> UpdateTelefone(Telefone telefone)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(telefone);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Telefone/UpdateTelefone/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao atualizar o telefone!");
            }
        }

        public async Task<bool> InativaTelefone(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Telefone/InativaTelefone/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao inativar o telefone!");
            }
        }
    }
}