using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using PolarisContacts.CrossCutting.Helpers.Exceptions;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;
using System.Linq;
using System;

namespace PolarisContacts.Tests.Services
{
    public class EmailTests
    {
        private readonly IEmailRepository _emailRepository = Substitute.For<IEmailRepository>();
        private readonly IContatoService _contatoService = Substitute.For<IContatoService>();
        private readonly EmailService _emailService;

        public EmailTests()
        {
            _emailService = new EmailService(_emailRepository, _contatoService);
        }

        [Fact]
        public async Task GetEmailsByIdContato_DeveRetornarEmails_QuandoIdContatoEhValido()
        {
            // Arrange
            int idContato = 1;
            var emails = new List<Email>
            {
                new Email { Id = 1, EnderecoEmail = "test1@example.com", IdContato = idContato, Ativo = true },
                new Email { Id = 2, EnderecoEmail = "test2@example.com", IdContato = idContato, Ativo = true }
            };
            _emailRepository.GetEmailsByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Email>>(emails));

            // Act
            var result = await _emailService.GetEmailsByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetEmailsByIdContato_DeveLancarInvalidIdException_QuandoIdContatoEhInvalido()
        {
            // Arrange
            int invalidIdContato = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _emailService.GetEmailsByIdContato(invalidIdContato));
        }

        [Fact]
        public async Task GetEmailById_DeveRetornarEmail_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;
            var email = new Email { Id = id, EnderecoEmail = "test@example.com", IdContato = 1, Ativo = true };
            _emailRepository.GetEmailById(id).Returns(Task.FromResult(email));

            // Act
            var result = await _emailService.GetEmailById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEmailById_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _emailService.GetEmailById(invalidId));
        }

        [Fact]
        public async Task GetEmailById_DeveLancarEmailNotFoundException_QuandoEmailNaoEncontrado()
        {
            // Arrange
            int id = 1;
            _emailRepository.GetEmailById(id).Returns(Task.FromResult<Email>(null));

            // Act & Assert
            await Assert.ThrowsAsync<EmailNotFoundException>(() => _emailService.GetEmailById(id));
        }

        [Fact]
        public async Task UpdateEmail_DeveChamarRepositorio_QuandoEmailEhValido()
        {
            // Arrange
            var email = new Email { Id = 1, EnderecoEmail = "updated@example.com", IdContato = 1, Ativo = true };

            // Act
            await _emailService.UpdateEmail(email);

            // Assert
            await _emailRepository.Received(1).UpdateEmail(email);
        }

        [Fact]
        public async Task UpdateEmail_DeveLancarArgumentNullException_QuandoEmailEhInvalido()
        {
            // Arrange
            Email invalidEmail = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _emailService.UpdateEmail(invalidEmail));
        }

        [Fact]
        public async Task UpdateEmail_DeveLancarArgumentNullException_QuandoEmailIdEhInvalido()
        {
            // Arrange
            var invalidEmail = new Email { Id = 0, EnderecoEmail = "updated@example.com", IdContato = 1, Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _emailService.UpdateEmail(invalidEmail));
        }

        [Fact]
        public async Task DeleteEmail_DeveChamarRepositorio_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;

            // Act
            await _emailService.DeleteEmail(id);

            // Assert
            await _emailRepository.Received(1).DeleteEmail(id);
        }

        [Fact]
        public async Task DeleteEmail_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _emailService.DeleteEmail(invalidId));
        }
    }
}
