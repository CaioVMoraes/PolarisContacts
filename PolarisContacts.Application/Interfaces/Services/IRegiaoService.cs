using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IRegiaoService
    {
        Task<IEnumerable<Regiao>> GetAllRegioes();
        Task<Regiao> GetRegiaoById(int id);
        Task AddRegiao(Regiao regiao);
        Task UpdateRegiao(Regiao regiao);
        Task DeleteRegiao(int id);
    }
}
