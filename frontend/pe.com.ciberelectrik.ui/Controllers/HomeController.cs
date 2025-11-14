using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using pe.com.ciberelectrik.ui.Models;

namespace pe.com.ciberelectrik.ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly string userApiUrl = "https://localhost:44346/api-sorteo/users";
        private readonly string boletoApiUrl = "https://localhost:44346/api-sorteo/boleto";
        private readonly string participanteApiUrl = "https://localhost:44346/api-sorteo/participantes"; // API para participantes

        public async Task<ActionResult> Index()
        {
            var user = Session["User"];
            if (user == null)
                return RedirectToAction("Index", "Inicio");

            List<User> users = new List<User>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(userApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(jsonString);
                }
            }
            return View(users); // Lista de usuarios para pestaña usuarios
        }

        public async Task<ActionResult> Boletos()
        {
            var user = Session["User"];
            if (user == null)
                return RedirectToAction("Index", "Inicio");

            List<Boleto> boletos = new List<Boleto>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(boletoApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    boletos = JsonConvert.DeserializeObject<List<Boleto>>(jsonString);
                }
            }

            return View(boletos);
        }

        public async Task<ActionResult> Participantes()
        {
            var user = Session["User"];
            if (user == null)
                return RedirectToAction("Index", "Inicio");

            List<Participante> participantes = new List<Participante>();
            using (var client = new HttpClient())
            {
                // Asumo que para obtener todos los participantes usas la ruta sin id
                var response = await client.GetAsync(participanteApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    participantes = JsonConvert.DeserializeObject<List<Participante>>(jsonString);
                }
            }

            return View(participantes);
        }

        [HttpGet]
        public async Task<ActionResult> EditarBoleto(long id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:44346/api-sorteo/boleto/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var boleto = JsonConvert.DeserializeObject<Boleto>(json);
                    return View(boleto);
                }
            }
            return RedirectToAction("Boletos");
        }

        [HttpPost]
        public async Task<ActionResult> EditarBoleto(Boleto boleto)
        {
            boleto.Comprado = 1;

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(boleto);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:44346/api-sorteo/boleto/update", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Boletos");
                }
            }

            ModelState.AddModelError("", "No se pudo actualizar el boleto.");
            return View(boleto);
        }
    }
}