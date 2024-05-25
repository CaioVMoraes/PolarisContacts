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
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllUsuariosAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            return usuario;
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            await _usuarioRepository.AddUsuarioAsync(usuario);
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            if (usuario == null || usuario.Id <= 0)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var existingUsuario = await _usuarioRepository.GetUsuarioByIdAsync(usuario.Id);

            if (existingUsuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingUsuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

            if (existingUsuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _usuarioRepository.DeleteUsuarioAsync(id);
        }
    }
}

