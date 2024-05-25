using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllContatosAsync();
        Task<Contato> GetContatoByIdAsync(int id);
        Task AddContatoAsync(Contato contato);
        Task UpdateContatoAsync(Contato contato);
        Task DeleteContatoAsync(int id);

    }
}