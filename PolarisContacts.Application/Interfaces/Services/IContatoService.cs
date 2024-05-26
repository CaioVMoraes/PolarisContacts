using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<Contato>> GetAllContatos();
        Task<Contato> GetContatoById(int id);
        Task AddContato(Contato contato);
        Task UpdateContato(Contato contato);
        Task DeleteContato(int id);
    }
}