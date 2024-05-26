using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato);
        Task<Endereco> GetEnderecoById(int id);
        Task<bool> AddEndereco(Endereco endereco);
        Task<bool> UpdateEndereco(Endereco endereco);
        Task<bool> DeleteEndereco(int id);
    }
}