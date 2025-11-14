using System.Web.Mvc;

namespace pe.com.ciberelectrik.ui.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // Limpia la sesión actual
            Session.Clear();
            Session.Abandon();

            // Redirige al login (o donde lo necesites)
            return RedirectToAction("Index", "Inicio"); // Asegúrate que "Inicio" sea tu controlador de login
        }
    }
}