using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.CrossCutting.Helpers;
using PolarisContacts.Domain;

namespace PolarisContacts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AccountController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string senha)
        {
            string hashedPassword = Criptografia.ComputeSha256Hash(senha);
            Usuario usuario = await _usuarioService.GetUserByPasswordAsync(login, hashedPassword);
            if (usuario is not null)
            {
                HttpContext.Session.SetString("UserLogin", usuario.Login);
                HttpContext.Session.SetString("UserId", usuario.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Login ou senha inválidos.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string login, string senha, string confirmSenha)
        {
            if (senha != confirmSenha)
            {
                ViewBag.ErrorMessage = "As senhas não coincidem.";
                return View();
            }

            string hashedPassword = Criptografia.ComputeSha256Hash(senha);
            if (await _usuarioService.CreateUserAsync(login, hashedPassword))
            {
                ViewBag.SuccessMessage = "Usuário cadastrado com sucesso.";
                return View("Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Erro ao cadastrar usuário. Tente novamente.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string login, string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                ViewBag.ErrorMessage = "As senhas não coincidem.";
                return View();
            }

            string hashedOldPassword = Criptografia.ComputeSha256Hash(oldPassword);
            string hashedNewPassword = Criptografia.ComputeSha256Hash(newPassword);

            if (await _usuarioService.ChangeUserPasswordAsync(login, hashedOldPassword, hashedNewPassword))
            {
                ViewBag.SuccessMessage = "Senha alterada com sucesso.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Erro ao alterar a senha. Verifique suas credenciais.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
