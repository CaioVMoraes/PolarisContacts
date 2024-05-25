using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarisContacts.Domain
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Regiao")]
        public int IdRegiao { get; set; }

        [ForeignKey("Contato")]
        public int IdContato { get; set; }


        [Display(Name = "Tel Residencial")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string NumeroTelefoneResidencial { get; set; }
        public string NumeroTelefoneResidencialFormatado => string.Format("({0}) {1}-{2}", NumeroTelefoneResidencial.Substring(0, 5), NumeroTelefoneResidencial.Substring(5));

    }
}