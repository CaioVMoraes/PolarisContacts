using System.ComponentModel.DataAnnotations;

namespace PolarisContacts.Domain
{
    public class Endereco
    {
        [Display(Name = "Estado")]
        public string Estado { get; set; }


        [Display(Name = "Cidade")]
        public string Cidade { get; set; }


        [Display(Name = "Bairro")]
        public string Bairro { get; set; }


        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }


        [Display(Name = "Complemento")]
        public string Complemento { get; set; }


        [Display(Name = "Numero")]
        public string Numero { get; set; }


        [Display(Name = "CEP")]
        public string CEP { get; set; }
    }
}
