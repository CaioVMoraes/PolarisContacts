using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarisContacts.Domain
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Regiao")]
        [Required(ErrorMessage = "A região é obrigatória.")]
        public int IdRegiao { get; set; }

        [ForeignKey("Contato")]
        [Required(ErrorMessage = "O contato é obrigatório.")]
        public int IdContato { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O número do celular é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string NumeroTelefone { get; set; }


        public bool Ativo { get; set; }


        public Regiao Regiao { get; set; }
    }
}