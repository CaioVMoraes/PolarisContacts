using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class RegiaoRepository(IDatabaseConnection dbConnection) : IRegiaoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Regiao>> GetAll()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Regiao/GetAll");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Regiao/GetById/{idRegiao}");

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
