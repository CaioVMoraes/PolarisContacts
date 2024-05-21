using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        IEnumerable<Endereco> GetEnderecosByIdPessoa(int idPessoa);
    }
}