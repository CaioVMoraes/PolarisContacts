using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using PolarisContacts.Domain.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class RegiaoRepository(IOptions<UrlApis> urlApis) : IRegiaoRepository
    {
        private readonly UrlApis _urlApis = urlApis.Value;

        public async Task<IEnumerable<Regiao>> GetAll()
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Regiao/GetAll");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Regiao>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter regiões: {response.StatusCode}");
            }
        }

        public async Task<Regiao> GetById(int idRegiao)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignora erros de certificado
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync($"{_urlApis.ReadService}/Regiao/GetById/{idRegiao}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Regiao>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter região: {response.StatusCode}");
            }
        }
    }
}
