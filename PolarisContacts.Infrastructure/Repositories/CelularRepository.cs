using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class CelularRepository(IDatabaseConnection dbConnection) : ICelularRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE IdContato = @IdContato AND Ativo = 1";
            return await conn.QueryAsync<Celular>(query, new { IdContato = idContato });
        }

        public async Task<Celular> GetCelularById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE Id = @Id AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Celular>(query, new { Id = id });
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
