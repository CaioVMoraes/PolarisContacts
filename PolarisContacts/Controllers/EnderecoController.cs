using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Controllers
{
    public class EnderecoController(IEnderecoService enderecoService) : Controller
    {
        private readonly IEnderecoService _enderecoService = enderecoService;

        [HttpGet]
        public async Task<ActionResult> ModalEditEndereco(int idEndereco = 0)
        {
            Endereco endereco = await _enderecoService.GetEnderecoById(idEndereco);
            if (endereco == null)
            {
                throw new EnderecoNotFoundException();
            }

            return PartialView("_PartialEditEndereco", endereco);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEndereco(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoService.UpdateEndereco(endereco);
                return Json(new { success = true, message = "Alterado com sucesso!" });

            }
            return Json(new { success = false, message = "Dados inválidos. Verifique as informações e tente novamente." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            try
            {
                await _enderecoService.DeleteEndereco(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}