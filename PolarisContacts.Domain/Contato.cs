using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarisContacts.Domain
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Display(Name = "DDD")]
        [StringLength(3, ErrorMessage = "O DDD deve ter 3 dígitos.")]
        public string DDDTelefoneResidencial { get; set; }

        [Display(Name = "Tel Residencial")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string NumeroTelefoneResidencial { get; set; }
        public string NumeroTelefoneResidencialFormatado => string.Format("({0}) {1}-{2}", DDDTelefoneResidencial, NumeroTelefoneResidencial.Substring(0, 5), NumeroTelefoneResidencial.Substring(5));

        [Display(Name = "DDD")]
        [StringLength(3, ErrorMessage = "O DDD deve ter 3 dígitos.")]
        public string DDDCelular { get; set; }

        [Display(Name = "Celular")]
        [Phone(ErrorMessage = "Número de celular inválido.")]
        public string NumeroCelular { get; set; }
        public string NumeroCelularFormatado => string.Format("({0}) {1}-{2}", DDDCelular, NumeroCelular.Substring(0, 5), NumeroCelular.Substring(5));

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido.")]
        public string Email { get; set; }
    }
}