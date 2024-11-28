using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;
using static PolarisContacts.CrossCutting.Helpers.Exceptions.CustomExceptions;

namespace PolarisContacts.Controllers
{
    public class EmailController(IEmailService emailService) : Controller
    {
        private readonly IEmailService _emailService = emailService;

        [HttpGet]
        public async Task<ActionResult> ModalEditEmail(int idEmail = 0)
        {
            Email email = await _emailService.GetEmailById(idEmail);
            if (email == null)
            {
                throw new EmailNotFoundException();
            }

            return PartialView("_PartialEditEmail", email);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail([FromBody] Email email)
        {
            if (ModelState.IsValid)
            {
                await _emailService.UpdateEmail(email);
                return Json(new { success = true, message = "Alterado com sucesso!" });
            }
            return Json(new { success = false, message = "Erro ao atualizar o email." });
        }

        [HttpPost]
        public async Task<IActionResult> InativaEmail(int id)
        {
            try
            {
                await _emailService.InativaEmail(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
