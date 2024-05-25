using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface ICelularRepository
    {
        Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato);
        Task<Celular> GetCelularById(int id);
        Task AddCelular(Celular celular);
        Task UpdateCelular(Celular celular);
        Task DeleteCelular(int id);
    }
}
