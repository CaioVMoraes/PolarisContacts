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


    public class EnderecoRepository (DbConnection dbConnection) : IEnderecoRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;
        

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdPessoa(int idPessoa)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();
            
            string query = "SELECT * FROM Enderecos WHERE IdPessoa = @IdPessoa";

            return await conn.QueryAsync<Endereco>(query, new { IdPessoa = idPessoa });            
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();
            
            string query = "SELECT * FROM Enderecos WHERE Id = @Id";

            return await conn.QueryFirstOrDefaultAsync<Endereco>(query, new { Id = id });            
        }

        public async Task AddEndereco(Endereco endereco)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();
            
            string query = @"INSERT INTO Enderecos (IdPessoa, Rua, Numero, Cidade, Estado, Bairro, Complemento, CEP, Ativo) 
                                VALUES (@IdPessoa, @Rua, @Numero, @Cidade, @Estado, @Bairro, @Complemento, @CEP, @Ativo)";
            
            await conn.ExecuteAsync(query, endereco);
            
        }

        public async Task UpdateEndereco(Endereco endereco)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();
            
            string query = @"UPDATE Enderecos SET 
                                IdPessoa = @IdPessoa, Rua = @Rua, Numero = @Numero, Cidade = @Cidade, Estado = @Estado, 
                                Bairro = @Bairro, Complemento = @Complemento, CEP = @CEP, Ativo = @Ativo 
                                WHERE Id = @Id";

            await conn.ExecuteAsync(query, endereco);
            
        }

        public async Task DeleteEndereco(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();
            
            string query = "DELETE FROM Enderecos WHERE Id = @Id";

            await conn.ExecuteAsync(query, new { Id = id });
            
        }
    }
}
