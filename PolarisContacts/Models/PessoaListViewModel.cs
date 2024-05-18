using PolarisContacts.Domain;

namespace PolarisContacts.Models
{
    public class PessoaListViewModel
    {
        public IEnumerable<Pessoa> Pessoas { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPessoas { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalPessoas / PageSize);
    }
}