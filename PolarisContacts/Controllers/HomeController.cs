using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using PolarisContacts.Models;
using System.Diagnostics;

namespace PolarisContacts.Controllers
{
    public class HomeController(IPessoaService pessoaService) : Controller
    {
        private readonly IPessoaService _pessoaService = pessoaService;
        
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 15)
        {
            IEnumerable<Pessoa> pessoas = _pessoaService.GetPessoas();

            var totalContatos = pessoas.Count();

            var viewModel = new ContatoListViewModel
            {
                Pessoas = pessoas,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalContatos = totalContatos
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
