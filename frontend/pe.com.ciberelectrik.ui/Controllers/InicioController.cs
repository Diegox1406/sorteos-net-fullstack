using pe.com.ciberelectrik.ui.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace pe.com.ciberelectrik.ui.Controllers
{
    public class InicioController : Controller
    {
        private readonly string apiUrl = "https://localhost:44346/api-sorteo/users";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string usuario, string clave)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<User>>(json);

                    var user = usuarios.FirstOrDefault(u => u.Username == usuario && u.Password == clave);
                    if (user != null)
                    {
                        Session["User"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ViewBag.ErrorMessage = "Usuario o clave incorrecta";
            return View();
        }
    }
}