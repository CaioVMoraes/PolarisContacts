using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class UsuarioRepository(IDatabaseConnection dbConnection) : IUsuarioRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Usuarios";
            return await conn.QueryAsync<Usuario>(query);
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        }

        public async Task<bool> AddUsuario(Usuario usuario)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"INSERT INTO Usuarios (Login, Senha, Ativo) 
                             VALUES (@Login, @Senha, @Ativo)";

            return await conn.ExecuteAsync(query, usuario) > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Usuarios SET 
                             Login = @Login, Senha = @Senha, Ativo = @Ativo 
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, usuario) > 0;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "DELETE FROM Usuarios WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = id }) > 0;
        }
    }
}
