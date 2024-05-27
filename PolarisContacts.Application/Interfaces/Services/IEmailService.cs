using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato);
        Task<Email> GetEmailById(int id);
        //Task AddEmail(Email email);
        Task UpdateEmail(Email email);
        Task DeleteEmail(int id);
    }
}
