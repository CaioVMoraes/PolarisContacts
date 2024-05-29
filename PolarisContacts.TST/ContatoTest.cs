using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.CrossCutting.Helpers.Exceptions;
using PolarisContacts.Domain;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Tests.Services
{
    public class ContatoTests
    {
        private readonly IDatabaseConnection _dbConnection = Substitute.For<IDatabaseConnection>();
        private readonly IContatoRepository _contatoRepository = Substitute.For<IContatoRepository>();
        private readonly ITelefoneRepository _telefoneRepository = Substitute.For<ITelefoneRepository>();
        private readonly ICelularRepository _celularRepository = Substitute.For<ICelularRepository>();
        private readonly IEmailRepository _emailRepository = Substitute.For<IEmailRepository>();
        private readonly IEnderecoRepository _enderecoRepository = Substitute.For<IEnderecoRepository>();
        private readonly IRegiaoService _regiaoService = Substitute.For<IRegiaoService>();
        private readonly ContatoService _contatoService;

        public ContatoTests()
        {         
            _contatoService = new ContatoService(_dbConnection, _contatoRepository, _telefoneRepository, _celularRepository, _emailRepository, _enderecoRepository, _regiaoService);
        }

        [Fact]
        public async Task GetAllContatosByIdUsuario_DeveRetornarContatos_QuandoIdUsuarioEhValido()
        {
            // Arrange
            int idUsuario = 1;
            var contatos = new List<Contato>
            {
                new Contato { Id = 1, Nome = "Contato 1", IdUsuario = idUsuario, Ativo = true },
                new Contato { Id = 2, Nome = "Contato 2", IdUsuario = idUsuario, Ativo = true }
            };
            _contatoRepository.GetAllContatosByIdUsuario(idUsuario).Returns(Task.FromResult<IEnumerable<Contato>>(contatos));

            // Act
            var result = await _contatoService.GetAllContatosByIdUsuario(idUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetContatoByIdAsync_DeveRetornarContato_QuandoIdContatoEhValido()
        {
            // Arrange
            int idContato = 1;
            var contato = new Contato { Id = idContato, Nome = "Contato 1", IdUsuario = 1, Ativo = true };
            _contatoRepository.GetContatoById(idContato).Returns(Task.FromResult(contato));

            // Act
            var result = await _contatoService.GetContatoByIdAsync(idContato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(idContato, result.Id);
        }

        [Fact]
        public async Task AddContato_DeveAdicionarContato_QuandoContatoEhValido()
        {
            // Arrange
            var contato = new Contato { IdUsuario = 1, Nome = "Contato Teste", Ativo = true };
            _contatoRepository.AddContato(contato, Arg.Any<IDbConnection>(), Arg.Any<IDbTransaction>())
                .Returns(Task.FromResult(1));

            // Act
            var result = await _contatoService.AddContato(contato);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddContato_DeveLancarUsuarioNotFoundException_QuandoIdUsuarioEhInvalido()
        {
            // Arrange
            var contato = new Contato { IdUsuario = 0, Nome = "Contato Teste", Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioNotFoundException>(() => _contatoService.AddContato(contato));
        }

        [Fact]
        public async Task AddContato_DeveLancarNomeObrigatorioException_QuandoNomeEhInvalido()
        {
            // Arrange
            var contato = new Contato { IdUsuario = 1, Nome = "", Ativo = true };

            // Act & Assert
            await Assert.ThrowsAsync<NomeObrigatorioException>(() => _contatoService.AddContato(contato));
        }

        [Fact]
        public async Task UpdateContato_DeveChamarRepositorio_QuandoContatoEhValido()
        {
            // Arrange
            var contato = new Contato { Id = 1, Nome = "Contato Atualizado", IdUsuario = 1, Ativo = true };
            _contatoRepository.UpdateContato(contato).Returns(Task.FromResult(true));

            // Act
            var result = await _contatoService.UpdateContato(contato);

            // Assert
            Assert.True(result);
            await _contatoRepository.Received(1).UpdateContato(contato);
        }

        [Fact]
        public async Task DeleteContato_DeveChamarRepositorio_QuandoIdEhValido()
        {
            // Arrange
            int id = 1;
            _contatoRepository.DeleteContato(id).Returns(Task.FromResult(true));

            // Act
            var result = await _contatoService.DeleteContato(id);

            // Assert
            Assert.True(result);
            await _contatoRepository.Received(1).DeleteContato(id);
        }

        [Fact]
        public async Task DeleteContato_DeveLancarInvalidIdException_QuandoIdEhInvalido()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => _contatoService.DeleteContato(invalidId));
        }
    }
}
