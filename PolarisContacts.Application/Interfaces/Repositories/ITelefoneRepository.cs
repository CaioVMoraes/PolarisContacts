using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface ITelefoneRepository
    {
        Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato);
        Task<Telefone> GetTelefoneById(int id);
        Task<int> AddTelefone(Telefone telefone);
        Task<bool> UpdateTelefone(Telefone telefone);
        Task<bool> InativaTelefone(int id);
    }
}