using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class TelefoneService(ITelefoneRepository telefoneRepository, IContatoService contatoService, IRegiaoService regiaoService) : ITelefoneService
    {
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new InvalidIdException();
            }

            return await _telefoneRepository.GetTelefonesByIdContato(idContato);
        }

        public async Task<Telefone> GetTelefoneById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var telefone = await _telefoneRepository.GetTelefoneById(id);

            if (telefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            return telefone;
        }

        public async Task AddTelefone(Telefone telefone)
        {
            if (telefone == null)
            {
                throw new ArgumentNullException(nameof(telefone));
            }

            if (telefone.IdContato <= 0 || await _contatoService.GetContatoById(telefone.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (telefone.IdRegiao <= 0 || await _regiaoService.GetRegiaoById(telefone.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _telefoneRepository.AddTelefone(telefone);
        }

        public async Task UpdateTelefone(Telefone telefone)
        {
            if (telefone == null || telefone.Id <= 0)
            {
                throw new ArgumentNullException(nameof(telefone));
            }

            var existingTelefone = await _telefoneRepository.GetTelefoneById(telefone.Id);

            if (existingTelefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            if (telefone.IdContato <= 0 || await _contatoService.GetContatoById(telefone.IdContato) == null)
            {
                throw new ContatoNotFoundException();
            }

            if (telefone.IdRegiao <= 0 || await _regiaoService.GetRegiaoById(telefone.IdRegiao) == null)
            {
                throw new RegiaoNotFoundException();
            }

            await _telefoneRepository.UpdateTelefone(telefone);
        }

        public async Task DeleteTelefone(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingTelefone = await _telefoneRepository.GetTelefoneById(id);

            if (existingTelefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            await _telefoneRepository.DeleteTelefone(id);
        }
    }
}