using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface ICelularRepository
    {
        Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato);
        Task<Celular> GetCelularById(int id);
        Task<int> AddCelular(Celular celular);
        Task<bool> UpdateCelular(Celular celular);
        Task<bool> InativaCelular(int id);
    }
}
