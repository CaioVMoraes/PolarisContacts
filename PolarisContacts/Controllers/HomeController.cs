using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Domain;
using PolarisContacts.Models;
using System.Diagnostics;

namespace PolarisContacts.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Pessoa> pessoas = new List<Pessoa>
            {
                new Pessoa { Nome = "Mario", DataNascimento = new DateTime(2002, 7, 1), Genero = "Masculino" },
                new Pessoa { Nome = "Jose", DataNascimento = new DateTime(2001, 6, 2), Genero = "Masculino" },
                new Pessoa { Nome = "Maria", DataNascimento = new DateTime(2000, 5, 3), Genero = "Feminino" },
                new Pessoa { Nome = "Fulano", DataNascimento = new DateTime(1999, 4, 4), Genero = "Masculino" },
                new Pessoa { Nome = "Ciclano", DataNascimento = new DateTime(1998, 3, 5), Genero = "Masculino" }
            };
            var totalPessoas = 5;

            var viewModel = new PessoaListViewModel
            {
                Pessoas = pessoas,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPessoas = totalPessoas
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
