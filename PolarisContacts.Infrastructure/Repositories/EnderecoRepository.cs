using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.DatabaseConnection;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{


    public class EnderecoRepository(IDatabaseConnection dbConnection) : IEnderecoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;


        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Endereco/GetEnderecosByIdContato/{idContato}");

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
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Endereco/GetEnderecoById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Endereco>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter endereço: {response.StatusCode}");
            }
        }

        public async Task<int> AddEndereco(Endereco endereco, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"INSERT INTO Enderecos (IdContato, Logradouro, Numero, Cidade, Estado, Bairro, Complemento, CEP, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdContato, @Logradouro, @Numero, @Cidade, @Estado, @Bairro, @Complemento, @CEP, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"INSERT INTO Enderecos (IdContato, Logradouro, Numero, Cidade, Estado, Bairro, Complemento, CEP, Ativo) 
                            VALUES (@IdContato, @Logradouro, @Numero, @Cidade, @Estado, @Bairro, @Complemento, @CEP, @Ativo);
                            SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, endereco, transaction);

        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Enderecos SET 
                                Logradouro = @Logradouro, Numero = @Numero, Cidade = @Cidade, Estado = @Estado, 
                                Bairro = @Bairro, Complemento = @Complemento, CEP = @CEP 
                                WHERE Id = @Id";

            return await conn.ExecuteAsync(query, endereco) > 0;

        }

        public async Task<bool> DeleteEndereco(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Enderecos SET 
                             Ativo = 0
                             WHERE Id = @Id";

            return await conn.ExecuteAsync(query, new { Id = id }) > 0;

        }
    }
}
