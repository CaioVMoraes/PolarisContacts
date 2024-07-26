using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PolarisContacts.Filters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        private const string TestEnvironment = "Test";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();

            // Verifica se o ambiente é de teste
            var environment = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsEnvironment(TestEnvironment))
            {
                base.OnActionExecuting(context);
                return;
            }

            if (controller == "Account" && (action == "Login" || action == "Register"))
            {
                return; // Não aplicar o filtro nas ações de login e registro
            }

            if (context.HttpContext.Session.GetString("UserLogin") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
