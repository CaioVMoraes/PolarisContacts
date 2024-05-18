using System.ComponentModel.DataAnnotations;
using System;

namespace PolarisContacts.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome{ get; set; }


        [Display(Name = "Genero")]
        public string Genero { get; set; }


        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimento { get; set; }
    }
}
