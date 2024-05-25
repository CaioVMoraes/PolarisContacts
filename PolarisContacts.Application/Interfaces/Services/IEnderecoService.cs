using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContatoAsync(int idPessoa);
        Task<Endereco> GetEnderecoByIdAsync(int id);
        Task AddEnderecoAsync(Endereco endereco);
        Task UpdateEnderecoAsync(Endereco endereco);
        Task DeleteEnderecoAsync(int id);
    }
}