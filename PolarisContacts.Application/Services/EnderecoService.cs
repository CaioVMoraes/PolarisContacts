using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class EnderecoService(IEnderecoRepository enderecoRepository) : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;

        public IEnumerable<Endereco> GetEnderecosByIdPessoa(int idPessoa)
        {
            if (idPessoa <= 0)
            {
                throw new PessoaNotFoundException();
            }

            return _enderecoRepository.GetEnderecosByIdPessoa(idPessoa);
        }
    }
}