using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using PolarisContacts.Models;

namespace PolarisContacts.Controllers
{
    public class HomeController(IContatoService contatoService, IRegiaoService regiaoService) : Controller
    {
        private readonly IContatoService _contatoService = contatoService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 15)
        {
            try
            {
                IEnumerable<Contato> contatos = await _contatoService.GetAllContatos();

                var totalContatos = contatos.Count();

                var viewModel = new ContatoListViewModel
                {
                    Contatos = contatos,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalContatos = totalContatos
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = true;
                TempData["Message"] = ex.Message;
                return View("../Shared/TelaErro");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ModalNovoContato()
        {
            try
            {
                ViewBag.Regioes = await _regiaoService.GetAllRegioes();

                return PartialView("_PartialNovoContato", new Contato());
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
