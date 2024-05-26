using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllUsuarios();
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            return usuario;
        }

        public async Task AddUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            await _usuarioRepository.AddUsuario(usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.Id <= 0)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var existingUsuario = await _usuarioRepository.GetUsuarioById(usuario.Id);

            if (existingUsuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _usuarioRepository.UpdateUsuario(usuario);
        }

        public async Task DeleteUsuario(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingUsuario = await _usuarioRepository.GetUsuarioById(id);

            if (existingUsuario == null)
            {
                throw new UsuarioNotFoundException();
            }

            await _usuarioRepository.DeleteUsuario(id);
        }
    }
}

