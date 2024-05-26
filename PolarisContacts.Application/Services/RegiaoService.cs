using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class RegiaoService(IRegiaoRepository regiaoRepository) : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository = regiaoRepository;

        public async Task<IEnumerable<Regiao>> GetAllRegioes()
        {
            return await _regiaoRepository.GetAllRegioes();
        }

        public async Task<Regiao> GetRegiaoById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var regiao = await _regiaoRepository.GetRegiaoById(id);

            if (regiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            return regiao;
        }

        public async Task AddRegiao(Regiao regiao)
        {
            if (regiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.AddRegiao(regiao);
        }

        public async Task UpdateRegiao(Regiao regiao)
        {
            if (regiao == null || regiao.Id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingRegiao = await _regiaoRepository.GetRegiaoById(regiao.Id);

            if (existingRegiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.UpdateRegiao(regiao);
        }

        public async Task DeleteRegiao(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingRegiao = await _regiaoRepository.GetRegiaoById(id);

            if (existingRegiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.DeleteRegiao(id);
        }
    }
}