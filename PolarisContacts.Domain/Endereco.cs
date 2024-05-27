using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarisContacts.Domain
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Contato")]
        public int IdContato { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O logradouro não pode exceder 100 caracteres.")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "O número não pode exceder 10 caracteres.")]
        public string Numero { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cidade não pode exceder 50 caracteres.")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(50, ErrorMessage = "O estado deve ter 50 caracteres.")]
        public string Estado { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(50, ErrorMessage = "O bairro não pode exceder 50 caracteres.")]
        public string Bairro { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(50, ErrorMessage = "O complemento não pode exceder 50 caracteres.")]
        public string Complemento { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string CEP { get; set; }


        public bool Ativo { get; set; }
    }
}
