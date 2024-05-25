using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class EmailService(IEmailRepository emailRepository, IContatoService contatoService) : IEmailService
    {
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly IContatoService _contatoService = contatoService;

        public async Task<IEnumerable<Email>> GetEmailsByIdContatoAsync(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _emailRepository.GetEmailsByIdContatoAsync(idContato);
        }

        public async Task<Email> GetEmailByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var email = await _emailRepository.GetEmailByIdAsync(id);

            if (email == null)
            {
                throw new EmailNotFoundException();
            }

            return email;
        }

        public async Task AddEmailAsync(Email email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            // Validar entidades relacionadas
            if (email.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(email.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            await _emailRepository.AddEmailAsync(email);
        }

        public async Task UpdateEmailAsync(Email email)
        {
            if (email == null || email.Id <= 0)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var existingEmail = await _emailRepository.GetEmailByIdAsync(email.Id);

            if (existingEmail == null)
            {
                throw new EmailNotFoundException();
            }

            // Validar entidades relacionadas
            if (email.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(email.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            await _emailRepository.UpdateEmailAsync(email);
        }

        public async Task DeleteEmailAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingEmail = await _emailRepository.GetEmailByIdAsync(id);

            if (existingEmail == null)
            {
                throw new EmailNotFoundException();
            }

            await _emailRepository.DeleteEmailAsync(id);
        }
    }
}