using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface ITelefoneService
    {
        Task<IEnumerable<Telefone>> GetTelefonesByIdContatoAsync(int idContato);
        Task<Telefone> GetTelefoneByIdAsync(int id);
        Task AddTelefoneAsync(Telefone telefone);
        Task UpdateTelefoneAsync(Telefone telefone);
        Task DeleteTelefoneAsync(int id);
    }
}