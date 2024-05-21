using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        IEnumerable<Contato> GetContatosByIdPessoa(int idPessoa);
    }
}