﻿using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class CelularRepository(IDatabaseConnection dbConnection) : ICelularRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE IdContato = @IdContato";
            return await conn.QueryAsync<Celular>(query, new { IdContato = idContato });
        }

        public async Task<Celular> GetCelularById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Celulares WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Celular>(query, new { Id = id });
        }

        public async Task<int> AddCelular(Celular celular, IDbConnection connection, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Celulares (IdRegiao, IdContato, NumeroCelular, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdRegiao, @IdContato, @NumeroCelular, @Ativo)";

            return await connection.QuerySingleAsync<int>(query, celular, transaction);
        }

        public async Task<bool> UpdateCelular(Celular celular)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Celulares SET 
                             IdRegiao = @IdRegiao, IdContato = @IdContato, Numero = @NumeroCelular, Ativo = @Ativo 
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, celular) > 0;
        }

        public async Task<bool> DeleteCelular(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "DELETE FROM Celulares WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }

}
