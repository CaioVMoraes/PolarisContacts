using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface ICelularService
    {
        Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato);
        Task<Celular> GetCelularById(int id);
        //Task AddCelular(Celular celular);
        Task UpdateCelular(Celular celular);
        Task DeleteCelular(int id);
    }
}
