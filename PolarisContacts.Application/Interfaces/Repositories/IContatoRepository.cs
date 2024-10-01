using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario);
        Task<Contato> GetContatoById(int idContato);
        Task<IEnumerable<Contato>> SearchByUsuarioIdAndTerm(int idUsuario, string searchTerm);
        Task<int> AddContato(Contato contato);
        Task<bool> UpdateContato(Contato contato);
        Task<bool> InativaContato(int idContato);
    }
}