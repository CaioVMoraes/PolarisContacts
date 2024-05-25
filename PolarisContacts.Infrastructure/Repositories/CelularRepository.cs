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
        public class CelularRepository(DbConnection dbConnection) : ICelularRepository
        {
            private readonly DbConnection _dbConnection = dbConnection;

            public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
            {
                using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

                string query = "SELECT * FROM Celulares WHERE IdContato = @IdContato";
                return await conn.QueryAsync<Celular>(query, new { IdContato = idContato });                
            }

            public async Task<Celular> GetCelularById(int id)
            {
                using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

                string query = "SELECT * FROM Celulares WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Celular>(query, new { Id = id });
            }

            public async Task AddCelular(Celular celular)
            {
                using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

                string query = @"INSERT INTO Celulares (IdRegiao, IdContato, Numero, Ativo) 
                             VALUES (@IdRegiao, @IdContato, @Numero, @Ativo)";

                await conn.ExecuteAsync(query, celular);
            }

            public async Task UpdateCelular(Celular celular)
            {
                using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

                string query = @"UPDATE Celulares SET 
                             IdRegiao = @IdRegiao, IdContato = @IdContato, Numero = @Numero, Ativo = @Ativo 
                             WHERE Id = @Id";
                await conn.ExecuteAsync(query, celular);
            }

            public async Task DeleteCelular(int id)
            {
                using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

                string query = "DELETE FROM Celulares WHERE Id = @Id";
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }
 
}
