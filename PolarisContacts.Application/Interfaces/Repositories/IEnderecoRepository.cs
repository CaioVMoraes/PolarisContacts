using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContatoAsync(int idPessoa);
        Task<Endereco> GetEnderecoByIdAsync(int id);
        Task AddEnderecoAsync(Endereco endereco);
        Task UpdateEnderecoAsync(Endereco endereco);
        Task DeleteEnderecoAsync(int id);
    }
}