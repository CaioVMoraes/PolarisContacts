using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class UsuarioRepository(IDatabaseConnection dbConnection) : IUsuarioRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;

        public async Task<Usuario> GetUserByPasswordAsync(string login, string senha)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:7048/Usuario/GetUserByPasswordAsync?login={login}&senha={senha}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            else
            {
                throw new HttpRequestException($"Erro ao obter usuário: {response.StatusCode}");
            }
        }

        public async Task<bool> CreateUserAsync(string login, string senha)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "INSERT INTO Usuarios ([Login], Senha, Ativo) VALUES (@Login, @Senha, 1)";

            return await conn.ExecuteAsync(query, new { Login = login, Senha = senha }) > 0;
        }

        public async Task<bool> ChangeUserPasswordAsync(string login, string oldPassword, string newPassword)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "UPDATE Usuarios SET Senha = @NewPassword WHERE [Login] = @Login AND Senha = @OldPassword AND Ativo = 1";

            return await conn.ExecuteAsync(query, new { Login = login, OldPassword = oldPassword, NewPassword = newPassword }) > 0;
        }
    }
}
