using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class TelefoneRepository(IDatabaseConnection dbConnection) : ITelefoneRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Telefone/GetTelefonesByIdContato/{idContato}");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Telefone/GetTelefoneById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Telefone>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter telefones: {response.StatusCode}");
            }
        }

        public async Task<int> AddTelefone(Telefone telefone, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"INSERT INTO Telefones (IdRegiao, IdContato, NumeroTelefone, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdRegiao, @IdContato, @NumeroTelefone, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"INSERT INTO Telefones (IdRegiao, IdContato, NumeroTelefone, Ativo) 
                            VALUES (@IdRegiao, @IdContato, @NumeroTelefone, @Ativo);
                            SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, telefone, transaction);
        }

        public async Task<bool> UpdateTelefone(Telefone telefone)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Telefones SET 
                             IdRegiao = @IdRegiao, NumeroTelefone = @NumeroTelefone
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, telefone) > 0;
        }

        public async Task<bool> DeleteTelefone(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Telefones SET 
                             Ativo = 0
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }
}