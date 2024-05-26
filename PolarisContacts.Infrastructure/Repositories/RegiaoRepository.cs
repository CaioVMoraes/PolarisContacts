using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class RegiaoRepository(IDatabaseConnection dbConnection) : IRegiaoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Regiao>> GetAllRegioes()
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Regioes";
            return await conn.QueryAsync<Regiao>(query);
        }

        public async Task<Regiao> GetRegiaoById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Regioes WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Regiao>(query, new { Id = id });
        }

        public async Task<bool> AddRegiao(Regiao regiao)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"INSERT INTO Regioes (DDD, NomeRegiao, Ativo) 
                            VALUES (@DDD, @NomeRegiao, @Ativo)";

            return await conn.ExecuteAsync(query, regiao) > 0;
        }

        public async Task<bool> UpdateRegiao(Regiao regiao)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Regioes SET 
                            DDD = @DDD, NomeRegiao = @NomeRegiao, Ativo = @Ativo 
                            WHERE Id = @Id";
            return await conn.ExecuteAsync(query, regiao) > 0;
        }

        public async Task<bool> DeleteRegiao(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "DELETE FROM Regioes WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }
}
