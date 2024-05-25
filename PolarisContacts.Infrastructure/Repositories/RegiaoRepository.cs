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
    public class RegiaoRepository(DbConnection dbConnection) : IRegiaoRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Regiao>> GetAllRegioes()
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Regioes";
            return await conn.QueryAsync<Regiao>(query);
        }

        public async Task<Regiao> GetRegiaoById(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Regioes WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Regiao>(query, new { Id = id });
        }

        public async Task AddRegiao(Regiao regiao)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"INSERT INTO Regioes (DDD, NomeRegiao, Ativo) 
                            VALUES (@DDD, @NomeRegiao, @Ativo)";

            await conn.ExecuteAsync(query, regiao);
        }

        public async Task UpdateRegiao(Regiao regiao)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"UPDATE Regioes SET 
                            DDD = @DDD, NomeRegiao = @NomeRegiao, Ativo = @Ativo 
                            WHERE Id = @Id";
            await conn.ExecuteAsync(query, regiao);
        }

        public async Task DeleteRegiao(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "DELETE FROM Regioes WHERE Id = @Id";
            await conn.ExecuteAsync(query, new { Id = id });
        }
    }
}
