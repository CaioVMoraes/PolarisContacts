using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato);
        Task<Endereco> GetEnderecoById(int id);
        //Task AddEndereco(Endereco endereco);
        Task UpdateEndereco(Endereco endereco);
        Task InativaEndereco(int id);
    }
}