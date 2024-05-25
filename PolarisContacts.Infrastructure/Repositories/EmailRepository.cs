using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class EmailRepository(DbConnection dbConnection) : IEmailRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Emails WHERE IdContato = @IdContato";
            return await conn.QueryAsync<Email>(query, new { IdContato = idContato });
        }

        public async Task<Email> GetEmailById(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Emails WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Email>(query, new { Id = id });
        }

        public async Task AddEmail(Email email)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"INSERT INTO Emails (IdContato, EnderecoEmail, Ativo) 
                             VALUES (@IdContato, @EnderecoEmail, @Ativo)";

            await conn.ExecuteAsync(query, email);
        }

        public async Task UpdateEmail(Email email)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"UPDATE Emails SET 
                             IdContato = @IdContato, EnderecoEmail = @EnderecoEmail, Ativo = @Ativo 
                             WHERE Id = @Id";
            await conn.ExecuteAsync(query, email);
        }

        public async Task DeleteEmail(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "DELETE FROM Emails WHERE Id = @Id";
            await conn.ExecuteAsync(query, new { Id = id });
        }
    }
}
