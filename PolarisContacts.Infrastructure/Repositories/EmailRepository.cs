using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class EmailRepository(IDatabaseConnection dbConnection) : IEmailRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Emails WHERE IdContato = @IdContato  AND Ativo = 1";
            return await conn.QueryAsync<Email>(query, new { IdContato = idContato });
        }

        public async Task<Email> GetEmailById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Emails WHERE Id = @Id  AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Email>(query, new { Id = id });
        }

        public async Task<int> AddEmail(Email email, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"INSERT INTO Emails (IdContato, EnderecoEmail, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdContato, @EnderecoEmail, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"INSERT INTO Emails (IdContato, EnderecoEmail, Ativo) 
                            VALUES (@IdContato, @EnderecoEmail, @Ativo);
                            SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, email, transaction);
        }

        public async Task<bool> UpdateEmail(Email email)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Emails SET 
                             EnderecoEmail = @EnderecoEmail
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, email) > 0;
        }

        public async Task<bool> DeleteEmail(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Emails SET 
                             Ativo = 0
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }
}
