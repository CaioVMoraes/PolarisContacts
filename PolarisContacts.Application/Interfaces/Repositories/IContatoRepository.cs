using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario);
        Task<Contato> GetContatoById(int idContato);
        Task<int> AddContato(Contato contato, IDbConnection connection, IDbTransaction transaction);
        Task<bool> UpdateContato(Contato contato);
        Task<bool> DeleteContato(int idContato);
    }
}