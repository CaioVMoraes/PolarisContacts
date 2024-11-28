using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface ITelefoneService
    {
        Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato);
        Task<Telefone> GetTelefoneById(int id);
        //Task AddTelefone(Telefone telefone);
        Task UpdateTelefone(Telefone telefone);
        Task InativaTelefone(int id);
    }
}