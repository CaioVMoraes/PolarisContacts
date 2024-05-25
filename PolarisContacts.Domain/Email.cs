using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Domain
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Contato")]
        [Required(ErrorMessage = "O contato é obrigatório.")]
        public int IdContato { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
        public string EnderecoEmail { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "O status ativo é obrigatório.")]
        public bool Ativo { get; set; }
    }
}
