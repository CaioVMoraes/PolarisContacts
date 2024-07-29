using NSubstitute;
using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PolarisContacts.Tests.Services
{
    public class RegiaoTests
    {
        private readonly IRegiaoRepository _regiaoRepository = Substitute.For<IRegiaoRepository>();
        private readonly RegiaoService _regiaoService;

        public RegiaoTests()
        {
            _regiaoService = new RegiaoService(_regiaoRepository);
        }

        [Fact]
        public async Task GetAll_DeveRetornarTodasRegioes()
        {
            // Arrange
            var regioes = new List<Regiao>
            {
                new Regiao { Id = 1, DDD = "11", NomeRegiao = "São Paulo", Ativo = true },
                new Regiao { Id = 2, DDD = "21", NomeRegiao = "Rio de Janeiro", Ativo = true }
            };
            _regiaoRepository.GetAll().Returns(Task.FromResult<IEnumerable<Regiao>>(regioes));

            // Act
            var result = await _regiaoService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetById_DeveRetornarRegiao_QuandoIdEhValido()
        {
            // Arrange
            int idRegiao = 1;
            var regiao = new Regiao { Id = idRegiao, DDD = "11", NomeRegiao = "São Paulo", Ativo = true };
            _regiaoRepository.GetById(idRegiao).Returns(Task.FromResult(regiao));

            // Act
            var result = await _regiaoService.GetById(idRegiao);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(idRegiao, result.Id);
        }

        [Fact]
        public async Task GetById_DeveRetornarNull_QuandoRegiaoNaoExiste()
        {
            // Arrange
            int idRegiao = 1;
            _regiaoRepository.GetById(idRegiao).Returns(Task.FromResult<Regiao>(null));

            // Act
            var result = await _regiaoService.GetById(idRegiao);

            // Assert
            Assert.Null(result);
        }
    }
}
