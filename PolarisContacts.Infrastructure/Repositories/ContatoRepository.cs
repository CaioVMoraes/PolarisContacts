using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class ContatoRepository(DbConnection dbConnection) : IContatoRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;


        public async Task<IEnumerable<Contato>> GetAllContatos()
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Contatos";

            return await conn.QueryAsync<Contato>(query);
        }

        public async Task<Contato> GetContatoById(int idContato)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Contatos WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Contato>(query, new { Id = idContato });
        }


        public async Task AddContato(Contato contato)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"INSERT INTO Contatos (Nome, Ativo) 
                             VALUES (@Nome, @Ativo)";

            await conn.ExecuteAsync(query, contato);
        }

        public async Task UpdateContato(Contato contato)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"UPDATE Contatos SET 
                             Nome = @Nome, Ativo = @Ativo 
                             WHERE Id = @Id";
            await conn.ExecuteAsync(query, contato);
        }

        public async Task DeleteContato(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "DELETE FROM Contatos WHERE Id = @Id";
            await conn.ExecuteAsync(query, new { Id = id });
        }
    }
}