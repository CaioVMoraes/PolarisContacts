using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public IEnumerable<Endereco> GetEnderecosByIdPessoa(int idPessoa)
        {
            IEnumerable<Endereco> enderecos = new List<Endereco>
            {
                new Endereco
                {
                    IdPessoa = 1,
                    Id = 1,
                    Estado = "SP",
                    Cidade = "São Paulo",
                    Bairro = "Centro",
                    Logradouro = "Rua A",
                    Complemento = "Apto 101",
                    Numero = "123",
                    CEP = "01000-000"
                },
                new Endereco
                {
                    Id = 2,
                    IdPessoa = 1,
                    Estado = "SP",
                    Cidade = "São Paulo",
                    Bairro = "Jardins",
                    Logradouro = "Rua B",
                    Complemento = "Casa",
                    Numero = "456",
                    CEP = "02000-000"
                },
                new Endereco
                {
                    Id = 3,
                    IdPessoa = 2,
                    Estado = "RJ",
                    Cidade = "Rio de Janeiro",
                    Bairro = "Copacabana",
                    Logradouro = "Avenida Atlântica",
                    Complemento = "Apto 202",
                    Numero = "789",
                    CEP = "22000-000"
                }
            };

            return enderecos.Where(x => x.IdPessoa == idPessoa);
        }
    }
}