using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarisContacts.Domain
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }


        public IEnumerable<Celular> Celulares { get; set; }
        public IEnumerable<Telefone> Telefones { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
    }
}