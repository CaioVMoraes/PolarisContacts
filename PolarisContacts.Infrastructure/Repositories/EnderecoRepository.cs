using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
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

        public async Task<int> AddEndereco(Endereco endereco, IDbConnection connection, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Enderecos (IdContato, Logradouro, Numero, Cidade, Estado, Bairro, Complemento, CEP, Ativo) 
                             OUTPUT INSERTED.Id
                             VALUES (@IdContato, @Logradouro, @Numero, @Cidade, @Estado, @Bairro, @Complemento, @CEP, @Ativo)";

            return await connection.QuerySingleAsync<int>(query, endereco, transaction);

        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Enderecos SET 
                                Logradouro = @Logradouro, Numero = @Numero, Cidade = @Cidade, Estado = @Estado, 
                                Bairro = @Bairro, Complemento = @Complemento, CEP = @CEP 
                                WHERE Id = @Id";

            return await conn.ExecuteAsync(query, endereco) > 0;

        }

        public async Task<bool> DeleteEndereco(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Enderecos SET 
                             Ativo = 0
                             WHERE Id = @Id";

            return await conn.ExecuteAsync(query, new { Id = id }) > 0;

        }
    }
}
