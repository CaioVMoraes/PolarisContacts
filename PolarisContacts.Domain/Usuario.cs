using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string Senha { get; set; }

        public string NovaSenha { get; set; }

        public string ConfirmSenha { get; set; }

        public bool Ativo { get; set; }


        public IEnumerable<Contato> Contatos { get; set; }
    }
}
