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
    public class UsuarioRepository(DbConnection dbConnection) : IUsuarioRepository
    {
        private readonly DbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Usuarios";
            return await conn.QueryAsync<Usuario>(query);
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        }

        public async Task AddUsuario(Usuario usuario)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"INSERT INTO Usuarios (Login, Senha, Ativo) 
                             VALUES (@Login, @Senha, @Ativo)";

            await conn.ExecuteAsync(query, usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = @"UPDATE Usuarios SET 
                             Login = @Login, Senha = @Senha, Ativo = @Ativo 
                             WHERE Id = @Id";
            await conn.ExecuteAsync(query, usuario);
        }

        public async Task DeleteUsuario(int id)
        {
            using IDbConnection conn = await _dbConnection.AbrirConexaoAsync();

            string query = "DELETE FROM Usuarios WHERE Id = @Id";
            await conn.ExecuteAsync(query, new { Id = id });
        }
    }
}
