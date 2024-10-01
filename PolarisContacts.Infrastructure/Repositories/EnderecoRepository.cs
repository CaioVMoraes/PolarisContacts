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
    public class EnderecoRepository(IOptions<UrlApis> urlApis) : IEnderecoRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Endereco/GetEnderecosByIdContato/{idContato}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Endereco>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter endereços: {response.StatusCode}");
            }
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Endereco/GetEnderecoById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Endereco>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter endereço: {response.StatusCode}");
            }
        }

        public async Task<int> AddEndereco(Endereco endereco)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(endereco);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.CreateService}/Endereco/AddEndereco/", content);

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                throw new HttpRequestException($"Erro ao cadastrar o endereço!");
            }
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(endereco);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.UpdateService}/Endereco/UpdateEndereco/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao atualizar o endereço!");
            }
        }

        public async Task<bool> InativaEndereco(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.UpdateService}/Endereco/InativaEndereco/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao inativar o endereço!");
            }
        }
    }
}
