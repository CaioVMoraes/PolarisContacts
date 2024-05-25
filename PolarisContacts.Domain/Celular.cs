using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Domain
{
    public class Celular
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Regiao")]
        [Required(ErrorMessage = "A região é obrigatória.")]
        public int IdRegiao { get; set; }

        [ForeignKey("Contato")]
        [Required(ErrorMessage = "O contato é obrigatório.")]
        public int IdContato { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O número do celular é obrigatório.")]
        [StringLength(20, ErrorMessage = "O número do celular não pode exceder 20 caracteres.")]
        public string Numero { get; set; }
        public bool Ativo { get; set; }
    }
}
