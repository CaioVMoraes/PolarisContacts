using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> GetPessoas();
    }
}