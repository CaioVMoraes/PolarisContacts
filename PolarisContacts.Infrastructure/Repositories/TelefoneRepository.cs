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

            string query = "SELECT * FROM Telefones WHERE IdContato = @IdContato";
            return await conn.QueryAsync<Telefone>(query, new { IdContato = idContato });
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Telefones WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Telefone>(query, new { Id = id });
        }

        public async Task<bool> AddTelefone(Telefone telefone)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"INSERT INTO Telefones (IdRegiao, IdContato, Numero, Ativo) 
                             VALUES (@IdRegiao, @IdContato, @Numero, @Ativo)";

            return await conn.ExecuteAsync(query, telefone) > 0;
        }

        public async Task<bool> UpdateTelefone(Telefone telefone)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Telefones SET 
                             IdRegiao = @IdRegiao, IdContato = @IdContato, Numero = @Numero, Ativo = @Ativo 
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, telefone) > 0;
        }

        public async Task<bool> DeleteTelefone(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "DELETE FROM Telefones WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }
}