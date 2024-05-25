using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato);
        Task<Email> GetEmailById(int id);
        Task AddEmail(Email email);
        Task UpdateEmail(Email email);
        Task DeleteEmail(int id);
    }
}
