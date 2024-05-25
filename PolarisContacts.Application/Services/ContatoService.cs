using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class ContatoService(IContatoRepository contatoRepository, IUsuarioService usuarioService) : IContatoService
    {
        private readonly IContatoRepository _contatoRepository = contatoRepository;
        private readonly IUsuarioService _usuarioService = usuarioService;

        public async Task<IEnumerable<Contato>> GetAllContatosAsync()
        {
            return await _contatoRepository.GetAllContatosAsync();
        }

        public async Task<Contato> GetContatoByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var contato = await _contatoRepository.GetContatoByIdAsync(id);

            if (contato == null)
            {
                throw new ContatoNotFoundException();
            }

            return contato;
        }

        public async Task AddContatoAsync(Contato contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException(nameof(contato));
            }

            // Validar a entidade relacionada (Usuário)
            if (contato.IdUsuario <= 0 || await _usuarioService.GetUsuarioByIdAsync(contato.IdUsuario) == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _contatoRepository.AddContatoAsync(contato);
        }

        public async Task UpdateContatoAsync(Contato contato)
        {
            if (contato == null || contato.Id <= 0)
            {
                throw new ArgumentNullException(nameof(contato));
            }

            var existingContato = await _contatoRepository.GetContatoByIdAsync(contato.Id);

            if (existingContato == null)
            {
                throw new ContatoNotFoundException();
            }

            // Validar a entidade relacionada (Usuário)
            if (contato.IdUsuario <= 0 || await _usuarioService.GetUsuarioByIdAsync(contato.IdUsuario) == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _contatoRepository.UpdateContatoAsync(contato);
        }

        public async Task DeleteContatoAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingContato = await _contatoRepository.GetContatoByIdAsync(id);

            if (existingContato == null)
            {
                throw new ContatoNotFoundException();
            }

            await _contatoRepository.DeleteContatoAsync(id);
        }
    }
}