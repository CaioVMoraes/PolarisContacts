using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato);
        Task<Endereco> GetEnderecoById(int id);
        Task<int> AddEndereco(Endereco endereco, IDbConnection connection, IDbTransaction transaction);
        Task<bool> UpdateEndereco(Endereco endereco);
        Task<bool> DeleteEndereco(int id);
    }
}