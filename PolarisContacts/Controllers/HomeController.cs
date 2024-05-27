using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using PolarisContacts.Models;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

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
                int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                IEnumerable<Contato> contatos = await _contatoService.GetAllContatosByIdUsuario(idUsuario);

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
        public async Task<ActionResult> ModalDadosContato(int idContato = 0)
        {
            try
            {
                Contato contato;
                if (idContato == 0)
                {
                    contato = new Contato();
                }
                else
                {
                    contato = await _contatoService.GetContatoByIdAsync(idContato);
                    if (contato == null)
                    {
                        throw new ContatoNotFoundException();
                    }
                }

                ViewBag.Regioes = await _regiaoService.GetAll();

                return PartialView("_PartialDadosContato", contato);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = true;
                TempData["Message"] = ex.Message;
                return View("../Shared/TelaErro");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertContato(Contato contato)
        {
            try
            {
                contato.IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                if (ModelState.IsValid)
                {
                    await _contatoService.AddContato(contato);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = true;
                TempData["Message"] = ex.Message;
                return View("../Shared/TelaErro");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContato(Contato contato)
        {
            if (ModelState.IsValid)
            {
                await _contatoService.UpdateContato(contato);
            }

            return RedirectToAction("Index");
        }
    }
}
