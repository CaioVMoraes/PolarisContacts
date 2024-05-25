using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Services
{
    public class TelefoneService(ITelefoneRepository telefoneRepository, IContatoService contatoService, IRegiaoService regiaoService) : ITelefoneService
    {
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContatoAsync(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _telefoneRepository.GetTelefonesByIdContatoAsync(idContato);
        }

        public async Task<Telefone> GetTelefoneByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var telefone = await _telefoneRepository.GetTelefoneByIdAsync(id);

            if (telefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            return telefone;
        }

        public async Task AddTelefoneAsync(Telefone telefone)
        {
            if (telefone == null)
            {
                throw new ArgumentNullException(nameof(telefone));
            }

            if (telefone.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(telefone.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (telefone.IdRegiao <= 0 || await _regiaoService.GetRegiaoByIdAsync(telefone.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _telefoneRepository.AddTelefoneAsync(telefone);
        }

        public async Task UpdateTelefoneAsync(Telefone telefone)
        {
            if (telefone == null || telefone.Id <= 0)
            {
                throw new ArgumentNullException(nameof(telefone));
            }

            var existingTelefone = await _telefoneRepository.GetTelefoneByIdAsync(telefone.Id);

            if (existingTelefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            if (telefone.IdContato <= 0 || await _contatoService.GetContatoByIdAsync(telefone.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (telefone.IdRegiao <= 0 || await _regiaoService.GetRegiaoByIdAsync(telefone.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _telefoneRepository.UpdateTelefoneAsync(telefone);
        }

        public async Task DeleteTelefoneAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingTelefone = await _telefoneRepository.GetTelefoneByIdAsync(id);

            if (existingTelefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            await _telefoneRepository.DeleteTelefoneAsync(id);
        }
    }
}