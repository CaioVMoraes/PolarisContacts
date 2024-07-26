using Dapper;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class ContatoRepository(IDatabaseConnection dbConnection) : IContatoRepository
    {
        private readonly IDatabaseConnection _dbConnection = dbConnection;


        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Contatos WHERE IdUsuario = @IdUsuario  AND Ativo = 1 ORDER BY Nome";

            return await conn.QueryAsync<Contato>(query, new { IdUsuario = idUsuario });
        }

        public async Task<Contato> GetContatoById(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = "SELECT * FROM Contatos WHERE Id = @Id  AND Ativo = 1";
            return await conn.QueryFirstOrDefaultAsync<Contato>(query, new { Id = idContato });
        }

        public async Task<IEnumerable<Contato>> SearchByUsuarioIdAndTerm(int idUsuario, string searchTerm)
        {
            using (var connection = _dbConnection.AbrirConexao())
            {
                var query = @"
                            SELECT DISTINCT c.*
                            FROM Contatos c
                            LEFT JOIN Telefones t ON c.Id = t.IdContato
                            LEFT JOIN Celulares cl ON c.Id = cl.IdContato
                            LEFT JOIN Emails e ON c.Id = e.IdContato
                            LEFT JOIN Enderecos en ON c.Id = en.IdContato
                            LEFT JOIN Regioes rt ON t.IdRegiao = rt.Id
                            LEFT JOIN Regioes rcl ON cl.IdRegiao = rcl.Id
                            WHERE c.IdUsuario = @IdUsuario
                                AND (
                                    c.Nome LIKE @SearchTerm OR
                                    t.NumeroTelefone LIKE @SearchTerm OR
                                    cl.NumeroCelular LIKE @SearchTerm OR
                                    e.EnderecoEmail LIKE @SearchTerm OR
                                    en.Logradouro LIKE @SearchTerm OR
                                    en.CEP LIKE @SearchTerm OR
                                    en.Cidade LIKE @SearchTerm OR
                                    en.Estado LIKE @SearchTerm OR
                                    rt.DDD + t.NumeroTelefone LIKE @SearchTerm OR
                                    rcl.DDD + cl.NumeroCelular LIKE @SearchTerm OR
                                    REPLACE(REPLACE(REPLACE(t.NumeroTelefone, '(', ''), ')', ''), '-', '') LIKE '%' + REPLACE(REPLACE(REPLACE(@SearchTerm, '(', ''), ')', ''), '-', '') + '%' OR
                                    REPLACE(REPLACE(REPLACE(cl.NumeroCelular, '(', ''), ')', ''), '-', '') LIKE '%' + REPLACE(REPLACE(REPLACE(@SearchTerm, '(', ''), ')', ''), '-', '') + '%'
                                )
                            ORDER BY
                                c.Nome";

                return await connection.QueryAsync<Contato>(query, new { SearchTerm = "%" + searchTerm + "%", IdUsuario = idUsuario });
            }
        }

        public async Task<int> AddContato(Contato contato, IDbConnection connection, IDbTransaction transaction)
        {
            string query;
            var isSqlServer = connection.GetType() == typeof(SqlConnection);

            if (isSqlServer)
            {
                // SQL Server
                query = @"
            INSERT INTO Contatos (Nome, IdUsuario, Ativo)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @IdUsuario, @Ativo)";
            }
            else
            {
                // SQLite
                query = @"
            INSERT INTO Contatos (Nome, IdUsuario, Ativo)
            VALUES (@Nome, @IdUsuario, @Ativo);
            SELECT last_insert_rowid();";
            }

            return await connection.QuerySingleAsync<int>(query, contato, transaction);
        }


        public async Task<bool> UpdateContato(Contato contato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Contatos SET 
                             Nome = @Nome, Ativo = @Ativo 
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, contato) > 0;
        }

        public async Task<bool> DeleteContato(int idContato)
        {
            using IDbConnection conn = _dbConnection.AbrirConexao();

            string query = @"UPDATE Contatos SET 
                             Ativo = 0
                             WHERE Id = @Id";
            return await conn.ExecuteAsync(query, new { Id = idContato }) > 0;
        }
    }
}