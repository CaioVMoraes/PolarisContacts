using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace PolarisContacts.Domain
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [RegularExpression("Masculino|Feminino|Não especificado", ErrorMessage = "O gênero deve ser 'Masculino', 'Feminino' ou 'Não especificado'.")]
        public string Genero { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida.")]
        [AllowNull]
        public DateTime? DataNascimento { get; set; }

        public IEnumerable<Contato> Contatos { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
    }
}