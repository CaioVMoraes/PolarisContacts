using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Infrastructure.Repositories;
using System.Data;

public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = "DataSource=:memory:";

        services.AddSingleton<IDbConnection>(sp =>
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        });

        services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
    }
}
