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

        public async Task<IEnumerable<Regiao>> GetAll()
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            var query = "SELECT * FROM Regioes WHERE Ativo = 1";
            return await conn.QueryAsync<Regiao>(query);
        }

        public async Task<Regiao> GetById(int idRegiao)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            var query = "SELECT * FROM Regioes WHERE Ativo = 1 AND Id = @IdRegiao";
            return await conn.QueryFirstOrDefaultAsync<Regiao>(query, new { @IdRegiao = idRegiao });
        }
    }
}
