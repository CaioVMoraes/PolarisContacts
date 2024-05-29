using System.Threading.Tasks;
using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using PolarisContacts.CrossCutting.Helpers.Exceptions;
using Xunit;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Tests.Services
{
    public class UsuarioTests
    {
        private readonly IUsuarioRepository _usuarioRepository = Substitute.For<IUsuarioRepository>();
        private readonly UsuarioService _usuarioService;

        public UsuarioTests()
        {           
            _usuarioService = new UsuarioService(_usuarioRepository);
        }

        [Fact]
        public async Task GetUserByPasswordAsync_DeveRetornarUsuario_QuandoLoginESenhaSaoValidos()
        {
            // Arrange
            string login = "testUser";
            string senha = "testPassword";
            var usuario = new Usuario { Id = 1, Login = login, Senha = senha, Ativo = true };
            _usuarioRepository.GetUserByPasswordAsync(login, senha).Returns(Task.FromResult(usuario));

            // Act
            var result = await _usuarioService.GetUserByPasswordAsync(login, senha);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(login, result.Login);
        }

        [Fact]
        public async Task GetUserByPasswordAsync_DeveLancarLoginVazioException_QuandoLoginEhNuloOuVazio()
        {
            // Arrange
            string invalidLogin = null;
            string senha = "testPassword";

            // Act & Assert
            await Assert.ThrowsAsync<LoginVazioException>(() => _usuarioService.GetUserByPasswordAsync(invalidLogin, senha));
        }

        [Fact]
        public async Task GetUserByPasswordAsync_DeveLancarSenhaVaziaException_QuandoSenhaEhNulaOuVazia()
        {
            // Arrange
            string login = "testUser";
            string invalidSenha = null;

            // Act & Assert
            await Assert.ThrowsAsync<SenhaVaziaException>(() => _usuarioService.GetUserByPasswordAsync(login, invalidSenha));
        }

        [Fact]
        public async Task CreateUserAsync_DeveRetornarTrue_QuandoUsuarioEhCriadoComSucesso()
        {
            // Arrange
            string login = "newUser";
            string senha = "newPassword";
            _usuarioRepository.CreateUserAsync(login, senha).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioService.CreateUserAsync(login, senha);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateUserAsync_DeveLancarLoginVazioException_QuandoLoginEhNuloOuVazio()
        {
            // Arrange
            string invalidLogin = null;
            string senha = "newPassword";

            // Act & Assert
            await Assert.ThrowsAsync<LoginVazioException>(() => _usuarioService.CreateUserAsync(invalidLogin, senha));
        }

        [Fact]
        public async Task CreateUserAsync_DeveLancarSenhaVaziaException_QuandoSenhaEhNulaOuVazia()
        {
            // Arrange
            string login = "newUser";
            string invalidSenha = null;

            // Act & Assert
            await Assert.ThrowsAsync<SenhaVaziaException>(() => _usuarioService.CreateUserAsync(login, invalidSenha));
        }

        [Fact]
        public async Task ChangeUserPasswordAsync_DeveRetornarTrue_QuandoSenhaEhAlteradaComSucesso()
        {
            // Arrange
            string login = "existingUser";
            string oldPassword = "oldPassword";
            string newPassword = "newPassword";
            _usuarioRepository.GetUserByPasswordAsync(login, oldPassword).Returns(Task.FromResult(new Usuario { Login = login, Senha = oldPassword }));
            _usuarioRepository.ChangeUserPasswordAsync(login, oldPassword, newPassword).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioService.ChangeUserPasswordAsync(login, oldPassword, newPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ChangeUserPasswordAsync_DeveLancarLoginVazioException_QuandoLoginEhNuloOuVazio()
        {
            // Arrange
            string invalidLogin = null;
            string oldPassword = "oldPassword";
            string newPassword = "newPassword";

            // Act & Assert
            await Assert.ThrowsAsync<LoginVazioException>(() => _usuarioService.ChangeUserPasswordAsync(invalidLogin, oldPassword, newPassword));
        }

        [Fact]
        public async Task ChangeUserPasswordAsync_DeveLancarSenhaIncorretaException_QuandoOldPasswordEhNulaOuVazia()
        {
            // Arrange
            string login = "existingUser";
            string invalidOldPassword = null;
            string newPassword = "newPassword";

            // Act & Assert
            await Assert.ThrowsAsync<SenhaIncorretaException>(() => _usuarioService.ChangeUserPasswordAsync(login, invalidOldPassword, newPassword));
        }

        [Fact]
        public async Task ChangeUserPasswordAsync_DeveLancarSenhaVaziaException_QuandoNewPasswordEhNulaOuVazia()
        {
            // Arrange
            string login = "existingUser";
            string oldPassword = "oldPassword";
            string invalidNewPassword = null;

            // Act & Assert
            await Assert.ThrowsAsync<SenhaVaziaException>(() => _usuarioService.ChangeUserPasswordAsync(login, oldPassword, invalidNewPassword));
        }

        [Fact]
        public async Task ChangeUserPasswordAsync_DeveLancarSenhaIncorretaException_QuandoOldPasswordNaoCorresponde()
        {
            // Arrange
            string login = "existingUser";
            string oldPassword = "oldPassword";
            string newPassword = "newPassword";
            _usuarioRepository.GetUserByPasswordAsync(login, oldPassword).Returns(Task.FromResult<Usuario>(null));

            // Act & Assert
            await Assert.ThrowsAsync<SenhaIncorretaException>(() => _usuarioService.ChangeUserPasswordAsync(login, oldPassword, newPassword));
        }
    }
}
