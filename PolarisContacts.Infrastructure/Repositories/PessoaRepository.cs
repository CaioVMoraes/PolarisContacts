using PolarisContacts.Application.Interfaces.Repositories;
using PolarisContacts.Domain;
using System;
using System.Collections.Generic;

namespace PolarisContacts.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        public IEnumerable<Pessoa> GetPessoas()
        {
            IEnumerable<Pessoa> pessoas = new List<Pessoa>
            {
                new Pessoa
                {
                    Id = 1,
                    Nome = "João Silva",
                    Genero = "Masculino",
                    DataNascimento = new DateTime(1980, 5, 15)
                },
                new Pessoa
                {
                    Id = 2,
                    Nome = "Maria Oliveira",
                    Genero = "Feminino",
                    DataNascimento = new DateTime(1992, 8, 25)
                }
            };

            return pessoas;
        }
    }
}