using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class TelefoneRepository(DbConnection dbConnection) : ITelefoneRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Telefones WHERE IdContato = @IdContato";
            return await conn.QueryAsync<Telefone>(query, new { IdContato = idContato });
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Telefones WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Telefone>(query, new { Id = id });
        }

        public async Task AddTelefone(Telefone telefone)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"INSERT INTO Telefones (IdRegiao, IdContato, Numero, Ativo) 
                             VALUES (@IdRegiao, @IdContato, @Numero, @Ativo)";

            await conn.ExecuteAsync(query, telefone);
        }

        public async Task UpdateTelefone(Telefone telefone)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"UPDATE Telefones SET 
                             IdRegiao = @IdRegiao, IdContato = @IdContato, Numero = @Numero, Ativo = @Ativo 
                             WHERE Id = @Id";
            await conn.ExecuteAsync(query, telefone);
        }

        public async Task DeleteTelefone(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "DELETE FROM Telefones WHERE Id = @Id";
            await conn.ExecuteAsync(query, new { Id = id });
        }
    }
}