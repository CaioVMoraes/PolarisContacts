using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Controllers
{
    public class CelularController(ICelularService celularService, IRegiaoService regiaoService) : Controller
    {
        private readonly ICelularService _celularService = celularService;
        private readonly IRegiaoService _regiaoService = regiaoService;

        [HttpGet]
        public async Task<ActionResult> ModalEditCelular(int idCelular = 0)
        {
            Celular celular = await _celularService.GetCelularById(idCelular);
            if (celular == null)
            {
                throw new CelularNotFoundException();
            }

            celular.Regiao = await _regiaoService.GetById(celular.IdRegiao);

            ViewBag.Regioes = await _regiaoService.GetAll();

            return PartialView("_PartialEditCelular", celular);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCelular(Celular celular)
        {
            if (ModelState.IsValid)
            {
                await _celularService.UpdateCelular(celular);
                return Json(new { success = true, message = "Alterado com sucesso!" });
            }
            return Json(new { success = false, message = "Erro ao atualizar o celular." });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCelular(int id)
        {
            try
            {
                await _celularService.DeleteCelular(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
