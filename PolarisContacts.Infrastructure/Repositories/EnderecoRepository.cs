using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using PolarisContacts.Domain.Settings;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{


    public class EnderecoRepository(IDatabaseConnection dbConnection) : IEnderecoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;
        

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();
            
            string query = "SELECT * FROM Enderecos WHERE IdContato = @IdContato";

            return await conn.QueryAsync<Endereco>(query, new { IdContato = idContato });            
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();
            
            string query = "SELECT * FROM Enderecos WHERE Id = @Id";

            return await conn.QueryFirstOrDefaultAsync<Endereco>(query, new { Id = id });            
        }

        public async Task<bool> AddEndereco(Endereco endereco)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();
            
            string query = @"INSERT INTO Enderecos (IdContato, Rua, Numero, Cidade, Estado, Bairro, Complemento, CEP, Ativo) 
                                VALUES (@IdContato, @Rua, @Numero, @Cidade, @Estado, @Bairro, @Complemento, @CEP, @Ativo)";
            
            return await conn.ExecuteAsync(query, endereco) > 0;
            
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();
            
            string query = @"UPDATE Enderecos SET 
                                IdContato = @IdContato, Rua = @Rua, Numero = @Numero, Cidade = @Cidade, Estado = @Estado, 
                                Bairro = @Bairro, Complemento = @Complemento, CEP = @CEP, Ativo = @Ativo 
                                WHERE Id = @Id";

            return await conn.ExecuteAsync(query, endereco) > 0;
            
        }

        public async Task<bool> DeleteEndereco(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();
            
            string query = "DELETE FROM Enderecos WHERE Id = @Id";

            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
            
        }
    }
}
