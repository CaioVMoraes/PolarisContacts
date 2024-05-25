using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace PolarisContacts.Domain
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

    }
}