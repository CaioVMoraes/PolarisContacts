using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato);
        Task<Email> GetEmailById(int id);
        Task<bool> AddEmail(Email email);
        Task<bool> UpdateEmail(Email email);
        Task<bool> DeleteEmail(int id);
    }
}
