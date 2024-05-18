using System.ComponentModel.DataAnnotations;

namespace PolarisContacts.Domain
{
    public class Contato
    {

        [Display(Name = "DDD")]
        public string DDDTelefoneResidencial { get; set; }


        [Display(Name = "Tel Residencial")]
        public string NumeroTelefoneResidencial { get; set; }


        [Display(Name = "DDD")]
        public string DDDCelular { get; set; }


        [Display(Name = "Celular")]
        public string NumeroCelular { get; set; }


        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
