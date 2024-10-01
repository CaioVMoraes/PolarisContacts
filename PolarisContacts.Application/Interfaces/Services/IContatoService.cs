using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario);
        Task<Contato> GetContatoByIdAsync(int idContato);
        Task<IEnumerable<Contato>> SearchContatosByIdUsuario(int idUsuario, string searchTerm);
        Task<bool> AddContato(Contato contato);
        Task<bool> UpdateContato(Contato contato);
        Task<bool> InativaContato(int id);
    }
}