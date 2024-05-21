using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        IEnumerable<Endereco> GetEnderecosByIdPessoa(int idPessoa);
    }
}