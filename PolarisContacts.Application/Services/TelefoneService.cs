using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;

namespace PolarisContacts.Application.Services
{
    public class TelefoneService(ITelefoneRepository telefoneRepository, IContatoService contatoService, IEnderecoService enderecoService) : ITelefoneService
    {
        private readonly ITelefoneRepository _pessoaRepository = telefoneRepository;
        private readonly IContatoService _contatoService = contatoService;
        private readonly IEnderecoService _enderecoService = enderecoService;

        public IEnumerable<Contato> GetTelefones()
        {
          
        }
    }