using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PolarisContacts.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                return View();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #region CONSULTAS
        //[HttpGet]
        //public ActionResult ValidarCEP(string cep)
        //{
        //    var ret = false;

        //    var dado = iinformacoescomplementaresbll.ConsultarCEP(cep).Result;

        //    if (dado != null)
        //    {
        //        ret = true;
        //    }

        //    return Json(ret);
        //}
        //#endregion

        //#region CADASTRAMENTO
        //[HttpPost]
        //public async Task<IActionResult> Inserir(Pessoa pessoa)
        //{
        //    try
        //    {
        //        return Json(await ipessoabll.Inserir(pessoa));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion
    }
}
