using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface ICelularService
    {
        Task<IEnumerable<Celular>> GetCelularesByIdContatoAsync(int idContato);
        Task<Celular> GetCelularByIdAsync(int id);
        Task AddCelularAsync(Celular celular);
        Task UpdateCelularAsync(Celular celular);
        Task DeleteCelularAsync(int id);
    }
}
