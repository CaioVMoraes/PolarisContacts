using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;
using System.Data;
using System.Data.SqlClient;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class DatabaseConnection(IOptions<DbSettings> dbSettings) : IDatabaseConnection
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;


        public IDbConnection AbrirConexao()
        {
            SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString);
            connection.Open();
            return connection;
        }

    }
}

