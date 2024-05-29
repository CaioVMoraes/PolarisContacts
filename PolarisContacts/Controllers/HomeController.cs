using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using PolarisContacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContatoService _contatoService;
        private readonly IRegiaoService _regiaoService;

        public HomeController(IContatoService contatoService, IRegiaoService regiaoService)
        {
            _contatoService = contatoService;
            _regiaoService = regiaoService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 15, string searchTerm = null)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                IEnumerable<Contato> contatos;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    contatos = await _contatoService.SearchContatosByIdUsuario(idUsuario, searchTerm);
                }
                else
                {
                    contatos = await _contatoService.GetAllContatosByIdUsuario(idUsuario);
                }

                var totalContatos = contatos.Count();

                var viewModel = new ContatoListViewModel
                {
                    Contatos = contatos.Skip((pageNumber - 1) * pageSize).Take(pageSize),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalContatos = totalContatos,
                    SearchTerm = searchTerm
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
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(new { Errors = errors });
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _contatoService.UpdateContato(contato);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(new { Errors = errors });
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
        public async Task<IActionResult> DeleteContato(int id)
        {
            try
            {
                await _contatoService.DeleteContato(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
