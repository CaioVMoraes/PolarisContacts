using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        Task<IEnumerable<Regiao>> GetAllRegioes();
        Task<Regiao> GetRegiaoById(int id);
        Task<bool> AddRegiao(Regiao regiao);
        Task<bool> UpdateRegiao(Regiao regiao);
        Task<bool> DeleteRegiao(int id);
    }
}
