using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IContatoService
    {
        IEnumerable<Telefone> GetContatosByIdPessoa(int idPessoa);
    }
}