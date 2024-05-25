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
    public class RegiaoService(IRegiaoRepository regiaoRepository) : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository = regiaoRepository;

        public async Task<IEnumerable<Regiao>> GetAllRegioesAsync()
        {
            return await _regiaoRepository.GetAllRegioesAsync();
        }

        public async Task<Regiao> GetRegiaoByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var regiao = await _regiaoRepository.GetRegiaoByIdAsync(id);

            if (regiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            return regiao;
        }

        public async Task AddRegiaoAsync(Regiao regiao)
        {
            if (regiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.AddRegiaoAsync(regiao);
        }

        public async Task UpdateRegiaoAsync(Regiao regiao)
        {
            if (regiao == null || regiao.Id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingRegiao = await _regiaoRepository.GetRegiaoByIdAsync(regiao.Id);

            if (existingRegiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.UpdateRegiaoAsync(regiao);
        }

        public async Task DeleteRegiaoAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingRegiao = await _regiaoRepository.GetRegiaoByIdAsync(id);

            if (existingRegiao == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _regiaoRepository.DeleteRegiaoAsync(id);
        }
    }
}