using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        public IEnumerable<Contato> GetContatosByIdPessoa(int idPessoa)
        {
            IEnumerable<Contato> contatos = new List<Contato>
            {
                new Contato
                {
                    Id = 1,
                    IdPessoa = 1,
                    DDDTelefoneResidencial = "11",
                    NumeroTelefoneResidencial = "12345678",
                    DDDCelular = "11",
                    NumeroCelular = "987654321",
                    Email = "joao.silva@example.com"
                },
                new Contato
                {
                    Id = 2,
                    IdPessoa = 1,
                    DDDTelefoneResidencial = "11",
                    NumeroTelefoneResidencial = "87654321",
                    DDDCelular = "11",
                    NumeroCelular = "123456789",
                    Email = "joao.silva@work.com"
                },
                new Contato
                {
                    Id = 3,
                    IdPessoa = 2,
                    DDDTelefoneResidencial = "21",
                    NumeroTelefoneResidencial = "22334455",
                    DDDCelular = "21",
                    NumeroCelular = "998877665",
                    Email = "maria.oliveira@example.com"
                }
            };

            return contatos.Where(x => x.IdPessoa == idPessoa);
        }
    }
}