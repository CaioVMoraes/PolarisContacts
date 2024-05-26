using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class EnderecoService(IEnderecoRepository enderecoRepository) : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;

        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            if (idContato <= 0)
            {
                throw new ContatoNotFoundException();
            }

            return await _enderecoRepository.GetEnderecosByIdContato(idContato);
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var endereco = await _enderecoRepository.GetEnderecoById(id);

            if (endereco == null)
            {
                throw new EnderecoNotFoundException();
            }

            return endereco;
        }

        public async Task AddEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException(nameof(endereco));
            }

            await _enderecoRepository.AddEndereco(endereco);
        }

        public async Task UpdateEndereco(Endereco endereco)
        {
            if (endereco == null || endereco.Id <= 0)
            {
                throw new ArgumentNullException(nameof(endereco));
            }

            var existingEndereco = await _enderecoRepository.GetEnderecoById(endereco.Id);

            if (existingEndereco == null)
            {
                throw new EnderecoNotFoundException();
            }

            await _enderecoRepository.UpdateEndereco(endereco);
        }

        public async Task DeleteEndereco(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var existingEndereco = await _enderecoRepository.GetEnderecoById(id);

            if (existingEndereco == null)
            {
                throw new EnderecoNotFoundException();
            }

            await _enderecoRepository.DeleteEndereco(id);
        }
    }
}