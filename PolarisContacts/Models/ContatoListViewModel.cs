using PolarisContacts.Domain;

namespace PolarisContacts.Models
{
    public class ContatoListViewModel
    {
        public IEnumerable<Contato> Contatos { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalContatos { get; set; }

        public string SearchTerm { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalContatos / PageSize);
    }
}