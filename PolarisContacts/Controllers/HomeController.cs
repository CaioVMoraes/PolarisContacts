using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public ActionResult ModalNovoContato()
        {
            try
            {
                return PartialView("_PartialNovoContato", new Pessoa());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = true;
                TempData["Message"] = ex.Message;
                return View("../Shared/TelaErro");
            }
        }
    }
}
