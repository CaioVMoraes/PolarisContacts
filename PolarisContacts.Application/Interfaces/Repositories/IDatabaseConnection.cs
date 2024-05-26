using System.Data;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IDatabaseConnection
    {
        IDbConnection AbrirConexao();
    }
}
