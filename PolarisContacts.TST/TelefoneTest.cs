using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Tests.Services
{
    public class TelefoneTests
    {
        private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
        private readonly IContatoService _contatoService = Substitute.For<IContatoService>();
        private readonly IRegiaoService _regiaoService = Substitute.For<IRegiaoService>();
        private readonly TelefoneService _telefoneService;

        public TelefoneTests()
        {
            _telefoneService = new TelefoneService(_telefoneRepository, _contatoService, _regiaoService);
        }

        [Fact]
        public async Task GetTelefonesByIdContato_DeveRetornarTelefones_QuandoIdContatoEhValido()
        {
            // Arrange
            int idContato = 1;
            var telefones = new List<Telefone>
            {
                new Telefone { Id = 1, NumeroTelefone = "123456789", IdContato = idContato, Ativo = true },
                new Telefone { Id = 2, NumeroTelefone = "987654321", IdContato = idContato, Ativo = true }
            };
            _telefoneRepository.GetTelefonesByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Telefone>>(telefones));

            // Act
            var result = await _telefoneService.GetTelefonesByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTelefonesByIdContato_DeveLancarInvalidIdException_QuandoIdContatoEhInvalido()
        {
            // Arrange
            int invalidIdContato = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _telefoneService.GetTelefonesByIdContato(invalidIdContato));
        }

        [Fact]
        public async Task GetTelefoneById_DeveRetornarTelefone_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;
            var telefone = new Telefone { Id = id, NumeroTelefone = "123456789", IdContato = 1, Ativo = true };
            _telefoneRepository.GetTelefoneById(id).Returns(Task.FromResult(telefone));

            // Act
            var result = await _telefoneService.GetTelefoneById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetTelefoneById_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _telefoneService.GetTelefoneById(invalidId));
        }

        [Fact]
        public async Task GetTelefoneById_DeveLancarTelefoneNotFoundException_QuandoTelefoneNaoEncontrado()
        {
            // Arrange
            int id = 1;
            _telefoneRepository.GetTelefoneById(id).Returns(Task.FromResult<Telefone>(null));

            // Act & Assert
            await Assert.ThrowsAsync<TelefoneNotFoundException>(() => _telefoneService.GetTelefoneById(id));
        }

        [Fact]
        public async Task UpdateTelefone_DeveChamarRepositorio_QuandoTelefoneEhValido()
        {
            // Arrange
            var telefone = new Telefone { Id = 1, NumeroTelefone = "987654321", IdContato = 1, Ativo = true };

            // Act
            await _telefoneService.UpdateTelefone(telefone);

            // Assert
            await _telefoneRepository.Received(1).UpdateTelefone(telefone);
        }

        [Fact]
        public async Task UpdateTelefone_DeveLancarArgumentNullException_QuandoTelefoneEhInvalido()
        {
            // Arrange
            Telefone invalidTelefone = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _telefoneService.UpdateTelefone(invalidTelefone));
        }

        [Fact]
        public async Task UpdateTelefone_DeveLancarArgumentNullException_QuandoTelefoneIdEhInvalido()
        {
            // Arrange
            var invalidTelefone = new Telefone { Id = 0, NumeroTelefone = "987654321", IdContato = 1, Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _telefoneService.UpdateTelefone(invalidTelefone));
        }

        [Fact]
        public async Task DeleteTelefone_DeveChamarRepositorio_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;

            // Act
            await _telefoneService.DeleteTelefone(id);

            // Assert
            await _telefoneRepository.Received(1).DeleteTelefone(id);
        }

        [Fact]
        public async Task DeleteTelefone_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _telefoneService.DeleteTelefone(invalidId));
        }
    }
}
