using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllContatos();
        Task<Contato> GetContatoById(int idContato);
        Task<bool> AddContato(Contato contato);
        Task<bool> UpdateContato(Contato contato);
        Task<bool> DeleteContato(int idContato);

    }
}