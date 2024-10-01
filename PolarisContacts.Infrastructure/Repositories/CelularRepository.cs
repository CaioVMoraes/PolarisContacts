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
    public class CelularRepository(IOptions<UrlApis> urlApis) : ICelularRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Celular/GetCelularesByIdContato/{idContato}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Celular>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter celulares: {response.StatusCode}");
            }
        }

        public async Task<Celular> GetCelularById(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Celular/GetCelularById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Celular>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter celular: {response.StatusCode}");
            }
        }

        public async Task<int> AddCelular(Celular celular)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(celular);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.CreateService}/Celular/AddCelular/", content);

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                throw new HttpRequestException($"Erro ao cadastrar o celular!");
            }
        }

        public async Task<bool> UpdateCelular(Celular celular)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(celular);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Celular/UpdateCelular/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao atualizar o celular!");
            }
        }

        public async Task<bool> InativaCelular(int id)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Celular/InativaCelular/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao inativar o celular!");
            }
        }
    }
}
