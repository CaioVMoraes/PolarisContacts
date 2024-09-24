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
    public class ContatoRepository(IDatabaseConnection dbConnection) : IContatoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;


        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Contato/GetAllContatosByIdUsuario/{idUsuario}");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Contato/GetContatoById/{idContato}");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Celular/SearchByUsuarioIdAndTerm/{idUsuario}?searchTerm={searchTerm}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Contato>>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter resultados: {response.StatusCode}");
            }
        }

        public async Task<int> AddContato(Contato contato, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"INSERT INTO Contatos (Nome, IdUsuario, Ativo)
                          OUTPUT INSERTED.Id
                          VALUES (@Nome, @IdUsuario, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"INSERT INTO Contatos (Nome, IdUsuario, Ativo)
                          VALUES (@Nome, @IdUsuario, @Ativo);
                          SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, contato, transaction);
        }


        public async Task<bool> UpdateContato(Contato contato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Contatos SET 
                             Nome = @Nome, Ativo = @Ativo 
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, contato) > 0;
        }

        public async Task<bool> DeleteContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Contatos SET 
                             Ativo = 0
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = idContato }) > 0;
        }
    }
}