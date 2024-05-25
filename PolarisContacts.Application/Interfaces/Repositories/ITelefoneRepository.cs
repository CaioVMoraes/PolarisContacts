using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface ITelefoneRepository
    {
        Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato);
        Task<Telefone> GetTelefoneById(int id);
        Task AddTelefone(Telefone telefone);
        Task UpdateTelefone(Telefone telefone);
        Task DeleteTelefone(int id);
    }
}