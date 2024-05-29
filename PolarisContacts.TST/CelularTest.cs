using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using PolarisContacts.CrossCutting.Helpers.Exceptions;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;
using System;

namespace PolarisContacts.Tests.Services
{
    public class CelularTest
    {
        private readonly ICelularRepository _celularRepository = Substitute.For<ICelularRepository>();
        private readonly IContatoService _contatoService = Substitute.For<IContatoService>();
        private readonly IRegiaoService _regiaoService = Substitute.For<IRegiaoService>();
        private readonly CelularService _celularService;

        public CelularTest()
        {                      
            _celularService = new CelularService(_celularRepository, _contatoService, _regiaoService);
        }


        // Método GetCelularesByIdContato
        [Fact]
        public async Task GetCelularesByIdContato_DeveRetornarCelulares_QuandoIdContatoEhValido()
        {
            // Arrange
            int idContato = 1;
            var celulares = new List<Celular>
            {
                new Celular { Id = 1, NumeroCelular = "123456789", IdRegiao = 1, IdContato = idContato, Ativo = true },
                new Celular { Id = 2, NumeroCelular = "987654321", IdRegiao = 2, IdContato = idContato, Ativo = true }
            };
            _celularRepository.GetCelularesByIdContato(idContato).Returns(Task.FromResult<IEnumerable<Celular>>(celulares));

            // Act
            var result = await _celularService.GetCelularesByIdContato(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCelularesByIdContato_DeveLancarInvalidIdException_QuandoIdContatoEhInvalido()
        {
            // Arrange
            int invalidIdContato = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _celularService.GetCelularesByIdContato(invalidIdContato));
        }

        [Fact]
        public async Task GetCelularById_DeveLancarCelularNotFoundException_QuandoCelularNaoEncontrado()
        {
            // Arrange
            int id = 1;
            _celularRepository.GetCelularById(id).Returns(Task.FromResult<Celular>(null));

            // Act & Assert
            await Assert.ThrowsAsync<CelularNotFoundException>(() => _celularService.GetCelularById(id));
        }


        // Método UpdateCelular
        [Fact]
        public async Task UpdateCelular_DeveChamarRepositorio_QuandoCelularEhValido()
        {
            // Arrange
            var celular = new Celular { Id = 1, NumeroCelular = "123456789", IdRegiao = 1, IdContato = 1, Ativo = true };

            // Act
            await _celularService.UpdateCelular(celular);

            // Assert
            await _celularRepository.Received(1).UpdateCelular(celular);
        }

        [Fact]
        public async Task UpdateCelular_DeveLancarArgumentNullException_QuandoCelularEhInvalido()
        {
            // Arrange
            Celular invalidCelular = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _celularService.UpdateCelular(invalidCelular));
        }

        [Fact]
        public async Task UpdateCelular_DeveLancarArgumentNullException_QuandoCelularIdEhInvalido()
        {
            // Arrange
            var invalidCelular = new Celular { Id = 0, NumeroCelular = "123456789", IdRegiao = 1, IdContato = 1, Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _celularService.UpdateCelular(invalidCelular));
        }


        //Método DeleteCelular
        [Fact]
        public async Task DeleteCelular_DeveChamarRepositorio_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;
            var celular = new Celular { Id = id, NumeroCelular = "123456789", IdRegiao = 1, IdContato = 1, Ativo = true };
            _celularRepository.GetCelularById(id).Returns(Task.FromResult(celular));

            // Act
            await _celularService.DeleteCelular(id);

            // Assert
            await _celularRepository.Received(1).DeleteCelular(id);
        }

        [Fact]
        public async Task DeleteCelular_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _celularService.DeleteCelular(invalidId));
        }

        [Fact]
        public async Task DeleteCelular_DeveLancarCelularNotFoundException_QuandoCelularNaoEncontrado()
        {
            // Arrange
            int id = 1;
            _celularRepository.GetCelularById(id).Returns(Task.FromResult<Celular>(null));

            // Act & Assert
            await Assert.ThrowsAsync<CelularNotFoundException>(() => _celularService.DeleteCelular(id));
        }

    }
}
