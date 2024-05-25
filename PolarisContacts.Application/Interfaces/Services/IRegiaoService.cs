using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IRegiaoService
    {
        Task<IEnumerable<Regiao>> GetAllRegioesAsync();
        Task<Regiao> GetRegiaoByIdAsync(int id);
        Task AddRegiaoAsync(Regiao regiao);
        Task UpdateRegiaoAsync(Regiao regiao);
        Task DeleteRegiaoAsync(int id);
    }
}
