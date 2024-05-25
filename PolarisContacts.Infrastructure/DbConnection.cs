using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain.Settings;

namespace PolarisContacts.Infrastructure
{
    public class DbConnection(IOptions<DbSettings> dbSettings)
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;


        public async Task<IDbConnection> AbrirConexaoAsync()
        {
            SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString);
            await connection.OpenAsync();
            return connection;
        }

    }
}

