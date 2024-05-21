using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetPessoas();
    }
}