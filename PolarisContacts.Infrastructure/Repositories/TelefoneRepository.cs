using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class TelefoneRepository(IDatabaseConnection dbConnection) : ITelefoneRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Telefones WHERE IdContato = @IdContato AND Ativo = 1";
            return await conn.QueryAsync<Telefone>(query, new { IdContato = idContato });
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Telefones WHERE Id = @Id  AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Telefone>(query, new { Id = id });
        }

        public async Task<int> AddTelefone(Telefone telefone, IDbConnection connection, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Telefones (IdRegiao, IdContato, NumeroTelefone, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdRegiao, @IdContato, @NumeroTelefone, @Ativo)";

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