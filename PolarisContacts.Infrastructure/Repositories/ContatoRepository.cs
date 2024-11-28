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
    public class ContatoRepository(IOptions<UrlApis> urlApis) : IContatoRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Contato/GetAllContatosByIdUsuario/{idUsuario}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Contato>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter contatos: {response.StatusCode}");
            }
        }

        public async Task<Contato> GetContatoById(int idContato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Contato/GetContatoById/{idContato}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Contato>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter contato: {response.StatusCode}");
            }
        }

        public async Task<IEnumerable<Contato>> SearchByUsuarioIdAndTerm(int idUsuario, string searchTerm)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Celular/SearchByUsuarioIdAndTerm/{idUsuario}?searchTerm={searchTerm}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Contato>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter resultados: {response.StatusCode}");
            }
        }

        public async Task<bool> AddContato(Contato contato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(contato);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_urlApis.CreateService}/Contato/AddContato/", content);

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateContato(Contato contato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(contato);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Contato/UpdateContato/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao atualizar o contato!");
            }
        }

        public async Task<bool> InativaContato(int idContato)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var jsonContent = JsonSerializer.Serialize(idContato);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_urlApis.UpdateService}/Contato/InativaContato/", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao inativar o contato!");
            }
        }
    }
}