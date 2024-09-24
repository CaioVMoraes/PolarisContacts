using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.DatabaseConnection;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class CelularRepository(IDatabaseConnection dbConnection) : ICelularRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Celular/GetCelularesByIdContato/{idContato}");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Celular/GetCelularById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Celular>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter celular: {response.StatusCode}");
            }
        }

        public async Task<int> AddCelular(Celular celular, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"INSERT INTO Celulares (IdRegiao, IdContato, NumeroCelular, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdRegiao, @IdContato, @NumeroCelular, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"INSERT INTO Celulares (IdRegiao, IdContato, NumeroCelular, Ativo) 
                            VALUES (@IdRegiao, @IdContato, @NumeroCelular, @Ativo);
                            SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, celular, transaction);
        }

        public async Task<bool> UpdateCelular(Celular celular)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Celulares SET 
                             IdRegiao = @IdRegiao, NumeroCelular = @NumeroCelular
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, celular) > 0;
        }

        public async Task<bool> DeleteCelular(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Celulares SET 
                             Ativo = 0
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }

}
