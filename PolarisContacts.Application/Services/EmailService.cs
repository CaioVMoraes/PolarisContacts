using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class EmailService(IEmailRepository emailRepository, IContatoService contatoService) : IEmailService
    {
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly IContatoService _contatoService = contatoService;

        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _emailRepository.GetEmailsByIdContato(idContato);
        }

        public async Task<Email> GetEmailById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var email = await _emailRepository.GetEmailById(id);

            if (email == null)
            {
                throw new EmailNotFoundException();
            }

            return email;
        }

        //public async Task AddEmail(Email email)
        //{
        //    if (email == null)
        //    {
        //        throw new ArgumentNullException(nameof(email));
        //    }

        //    await _emailRepository.AddEmail(email);
        //}

        public async Task UpdateEmail(Email email)
        {
            if (email == null || email.Id <= 0)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var existingEmail = await _emailRepository.GetEmailById(email.Id);

            if (existingEmail == null)
            {
                throw new EmailNotFoundException();
            }

            await _emailRepository.UpdateEmail(email);
        }

        public async Task DeleteEmail(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingEmail = await _emailRepository.GetEmailById(id);

            if (existingEmail == null)
            {
                throw new EmailNotFoundException();
            }

            await _emailRepository.DeleteEmail(id);
        }
    }
}