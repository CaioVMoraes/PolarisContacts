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
    public class CelularService(ICelularRepository celularRepository, IContatoService contatoService, IRegiaoService regiaoService) : ICelularService
    {
        private readonly ICelularRepository _celularRepository = celularRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IEnumerable<Celular>> GetCelularesByIdContatoAsync(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _celularRepository.GetCelularesByIdContatoAsync(idContato);
        }

        public async Task<Celular> GetCelularByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var celular = await _celularRepository.GetCelularByIdAsync(id);

            if (celular == null)
            {
                throw new CelularNotFoundException();
            }

            return celular;
        }

        public async Task AddCelularAsync(Celular celular)
        {
            if (celular == null)
            {
                throw new ArgumentNullException(nameof(celular));
            }

            // Validar entidades relacionadas
            if (celular.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(celular.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (celular.IdRegiao <= 0 || await _regiaoService.GetRegiaoByIdAsync(celular.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _celularRepository.AddCelularAsync(celular);
        }

        public async Task UpdateCelularAsync(Celular celular)
        {
            if (celular == null || celular.Id <= 0)
            {
                throw new ArgumentNullException(nameof(celular));
            }

            var existingCelular = await _celularRepository.GetCelularByIdAsync(celular.Id);

            if (existingCelular == null)
            {
                throw new CelularNotFoundException();
            }

            // Validar entidades relacionadas
            if (celular.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(celular.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (celular.IdRegiao <= 0 || await _regiaoService.GetRegiaoByIdAsync(celular.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _celularRepository.UpdateCelularAsync(celular);
        }

        public async Task DeleteCelularAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingCelular = await _celularRepository.GetCelularByIdAsync(id);

            if (existingCelular == null)
            {
                throw new CelularNotFoundException();
            }

            await _celularRepository.DeleteCelularAsync(id);
        }
    }
}