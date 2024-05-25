using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        Task<IEnumerable<Regiao>> GetAllRegioes();
        Task<Regiao> GetRegiaoById(int id);
        Task AddRegiao(Regiao regiao);
        Task UpdateRegiao(Regiao regiao);
        Task DeleteRegiao(int id);
    }
}
