using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdPessoa(int idPessoa);
        Task<Endereco> GetEnderecoById(int id);
        Task AddEndereco(Endereco endereco);
        Task UpdateEndereco(Endereco endereco);
        Task DeleteEndereco(int id);
    }
}