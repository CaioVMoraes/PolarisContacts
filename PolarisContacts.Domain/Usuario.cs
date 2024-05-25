using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisContacts.Domain
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "O login é obrigatório.")]
        [StringLength(20, ErrorMessage = "O login não pode exceder 20 caracteres.")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(20, ErrorMessage = "A senha não pode exceder 20 caracteres.")]
        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}
