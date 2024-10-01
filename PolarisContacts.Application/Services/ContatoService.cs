using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Application.Services
{
    public class ContatoService(IContatoRepository contatoRepository,
                                ITelefoneRepository telefoneRepository,
                                ICelularRepository celularRepository,
                                IEmailRepository emailRepository,
                                IEnderecoRepository enderecoRepository,
                                IRegiaoService regiaoService) : IContatoService
    {
        private readonly IContatoRepository _contatoRepository = contatoRepository;
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
        private readonly ICelularRepository _celularRepository = celularRepository;
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            IEnumerable<Contato> contatos = await _contatoRepository.GetAllContatosByIdUsuario(idUsuario);
            if (contatos is not null && contatos.Any())
            {
                contatos = await BuscaDadosContato(contatos);
            }
            return contatos;
        }

        public async Task<Contato> GetContatoByIdAsync(int idContato)
        {
            var contato = await _contatoRepository.GetContatoById(idContato);
            if (contato is not null)
            {
                contato.Telefones = await _telefoneRepository.GetTelefonesByIdContato(idContato);
                foreach (var telefone in contato.Telefones)
                {
                    telefone.Regiao = await _regiaoService.GetById(telefone.IdRegiao);
                }

                contato.Celulares = await _celularRepository.GetCelularesByIdContato(idContato);
                foreach (var celular in contato.Celulares)
                {
                    celular.Regiao = await _regiaoService.GetById(celular.IdRegiao);
                }

                contato.Emails = await _emailRepository.GetEmailsByIdContato(idContato);
                contato.Enderecos = await _enderecoRepository.GetEnderecosByIdContato(idContato);
            }
            return contato;
        }

        public async Task<IEnumerable<Contato>> SearchContatosByIdUsuario(int idUsuario, string searchTerm)
        {
            IEnumerable<Contato> contatos = await _contatoRepository.SearchByUsuarioIdAndTerm(idUsuario, searchTerm);
            if (contatos is not null && contatos.Any())
            {
                contatos = await BuscaDadosContato(contatos);
            }

            return contatos;
        }

        private async Task<IEnumerable<Contato>> BuscaDadosContato(IEnumerable<Contato> contatos)
        {
            foreach (var contato in contatos)
            {
                contato.Telefones = await _telefoneRepository.GetTelefonesByIdContato(contato.Id);
                foreach (var telefone in contato.Telefones)
                {
                    telefone.Regiao = await _regiaoService.GetById(telefone.IdRegiao);
                }

                contato.Celulares = await _celularRepository.GetCelularesByIdContato(contato.Id);
                foreach (var celular in contato.Celulares)
                {
                    celular.Regiao = await _regiaoService.GetById(celular.IdRegiao);
                }

                contato.Emails = await _emailRepository.GetEmailsByIdContato(contato.Id);

                contato.Enderecos = await _enderecoRepository.GetEnderecosByIdContato(contato.Id);
            }

            return contatos;
        }

        public async Task<bool> AddContato(Contato contato)
        {
            if (contato.IdUsuario <= 0)
            {
                throw new UsuarioNotFoundException();
            }
            if (string.IsNullOrEmpty(contato.Nome))
            {
                throw new NomeObrigatorioException();
            }

            await _contatoRepository.AddContato(contato);

            return true;
        }


        public async Task<bool> UpdateContato(Contato contato)
        {
            return await _contatoRepository.UpdateContato(contato);
        }

        public async Task<bool> InativaContato(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            return await _contatoRepository.InativaContato(id);
        }
    }
}