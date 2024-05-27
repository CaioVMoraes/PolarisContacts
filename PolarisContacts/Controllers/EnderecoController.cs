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
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEndereco(int idEndereco)
        {
            await _enderecoService.DeleteEndereco(idEndereco);
            return Ok();
        }
    }
}