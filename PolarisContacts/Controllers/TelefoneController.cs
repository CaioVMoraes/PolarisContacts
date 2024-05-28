using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Controllers
{
    public class TelefoneController(ITelefoneService telefoneService, IRegiaoService regiaoService) : Controller
    {
        private readonly ITelefoneService _telefoneService = telefoneService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        [HttpGet]
        public async Task<ActionResult> ModalEditTelefone(int idTelefone = 0)
        {
            Telefone telefone = await _telefoneService.GetTelefoneById(idTelefone);
            if (telefone == null)
            {
                throw new TelefoneNotFoundException();
            }

            ViewBag.Regioes = await _regiaoService.GetAll();

            return PartialView("_PartialEditTelefone", telefone);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTelefone(Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                await _telefoneService.UpdateTelefone(telefone);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTelefone(int id)
        {
            try
            {
                await _telefoneService.DeleteTelefone(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
