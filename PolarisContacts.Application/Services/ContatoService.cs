using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class ContatoService(IContatoRepository contatoRepository) : IContatoService
    {
        private readonly IContatoRepository _contatoRepository = contatoRepository;

        public IEnumerable<Telefone> GetContatosByIdPessoa(int idPessoa)
        {
            if (idPessoa <= 0)
            {
                throw new PessoaNotFoundException();
            }

            return _contatoRepository.GetContatosByIdPessoa(idPessoa);
        }
    }
}