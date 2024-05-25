using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Services
{
    public class PessoaService(ITelefoneRepository pessoaRepository, IContatoService contatoService, IEnderecoService enderecoService) : IPessoaService
    {
        private readonly ITelefoneRepository _pessoaRepository = pessoaRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IEnderecoService _enderecoService = enderecoService;

        public IEnumerable<Contato> GetPessoas()
        {
            IEnumerable<Contato> pessoas = _pessoaRepository.GetPessoas();

            foreach (Contato pessoa in pessoas)
            {
                pessoa.Contatos = _contatoService.GetContatosByIdPessoa(pessoa.Id);
                pessoa.Enderecos = _enderecoService.GetEnderecosByIdPessoa(pessoa.Id);
            }

            return pessoas;
        }
    }