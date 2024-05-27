using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class ContatoRepository(IDatabaseConnection dbConnection) : IContatoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;


        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Contatos WHERE IdUsuario = @IdUsuario";

            return await conn.QueryAsync<Contato>(query, new { IdUsuario = idUsuario });
        }

        public async Task<Contato> GetContatoById(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Contatos WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Contato>(query, new { Id = idContato });
        }


        public async Task<int> AddContato(Contato contato, IDbConnection connection, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Contatos (Nome, IdUsuario, Ativo)
                             OUTPUT INSERTED.Id
                             VALUES (@Nome, @IdUsuario, @Ativo)";

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

            string query = "DELETE FROM Contatos WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = idContato }) > 0;
        }
    }
}