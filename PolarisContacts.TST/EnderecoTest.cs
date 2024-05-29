using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using PolarisContacts.CrossCutting.Helpers.Exceptions;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;
using System.Linq;
using System;

namespace PolarisContacts.Tests.Services
{
    public class EnderecoTests
    {
        private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
        private readonly EnderecoService _enderecoService;

        public EnderecoTests()
        {          
            _enderecoService = new EnderecoService(_enderecoRepository);
        }

        [Fact]
        public async Task GetEnderecosByIdContato_DeveRetornarEnderecos_QuandoIdContatoEhValido()
        {
            // Arrange
            int idContato = 1;
            var enderecos = new List<Endereco>
            {
                new Endereco { Id = 1, Logradouro = "Rua 1", IdContato = idContato, Ativo = true },
                new Endereco { Id = 2, Logradouro = "Rua 2", IdContato = idContato, Ativo = true }
            };
            _enderecoRepository.GetEnderecosByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Endereco>>(enderecos));

            // Act
            var result = await _enderecoService.GetEnderecosByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetEnderecosByIdContato_DeveLancarContatoNotFoundException_QuandoIdContatoEhInvalido()
        {
            // Arrange
            int invalidIdContato = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ContatoNotFoundException>(() => _enderecoService.GetEnderecosByIdContato(invalidIdContato));
        }

        [Fact]
        public async Task GetEnderecoById_DeveRetornarEndereco_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;
            var endereco = new Endereco { Id = id, Logradouro = "Rua 1", IdContato = 1, Ativo = true };
            _enderecoRepository.GetEnderecoById(id).Returns(Task.FromResult(endereco));

            // Act
            var result = await _enderecoService.GetEnderecoById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEnderecoById_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _enderecoService.GetEnderecoById(invalidId));
        }

        [Fact]
        public async Task GetEnderecoById_DeveLancarEnderecoNotFoundException_QuandoEnderecoNaoEncontrado()
        {
            // Arrange
            int id = 1;
            _enderecoRepository.GetEnderecoById(id).Returns(Task.FromResult<Endereco>(null));

            // Act & Assert
            await Assert.ThrowsAsync<EnderecoNotFoundException>(() => _enderecoService.GetEnderecoById(id));
        }

        [Fact]
        public async Task UpdateEndereco_DeveChamarRepositorio_QuandoEnderecoEhValido()
        {
            // Arrange
            var endereco = new Endereco { Id = 1, Logradouro = "Rua Atualizada", IdContato = 1, Ativo = true };

            // Act
            await _enderecoService.UpdateEndereco(endereco);

            // Assert
            await _enderecoRepository.Received(1).UpdateEndereco(endereco);
        }

        [Fact]
        public async Task UpdateEndereco_DeveLancarArgumentNullException_QuandoEnderecoEhInvalido()
        {
            // Arrange
            Endereco invalidEndereco = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _enderecoService.UpdateEndereco(invalidEndereco));
        }

        [Fact]
        public async Task UpdateEndereco_DeveLancarArgumentNullException_QuandoEnderecoIdEhInvalido()
        {
            // Arrange
            var invalidEndereco = new Endereco { Id = 0, Logradouro = "Rua Atualizada", IdContato = 1, Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _enderecoService.UpdateEndereco(invalidEndereco));
        }

        [Fact]
        public async Task DeleteEndereco_DeveChamarRepositorio_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;

            // Act
            await _enderecoService.DeleteEndereco(id);

            // Assert
            await _enderecoRepository.Received(1).DeleteEndereco(id);
        }

        [Fact]
        public async Task DeleteEndereco_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _enderecoService.DeleteEndereco(invalidId));
        }
    }
}
