using Newtonsoft.Json;
using pe.com.ciberelectrik.ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace pe.com.ciberelectrik.ui.Controllers
{
    public class BoletoController : Controller
    {
        private readonly string apiUrl = "https://localhost:44346/api-sorteo/boleto";
        private readonly string apiApartadoUrl = "https://localhost:44346/api-sorteo/apartados/register";
        private readonly string apiParticipanteUrl = "https://localhost:44346/api-sorteo/participantes/register";

        // GET: Boleto
        public async Task<ActionResult> Index()
        {
            List<Boleto> boletosDisponibles = new List<Boleto>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var todosLosBoletos = JsonConvert.DeserializeObject<List<Boleto>>(json);

                    // Filtrar solo boletos donde FechaApartado y FechaCompra son null
                    boletosDisponibles = todosLosBoletos
                        .Where(b => b.FechaApartado == null && b.FechaCompra == null)
                        .ToList();
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de boletos desde la API.";
                }
            }

            return View(boletosDisponibles);
        }

        [HttpPost]
        public ActionResult Apartar(string idsSeleccionados)
        {
            if (string.IsNullOrEmpty(idsSeleccionados))
            {
                return RedirectToAction("Index");
            }

            var ids = idsSeleccionados.Split(',').Select(long.Parse).ToList();

            TempData["BoletosSeleccionados"] = ids;

            return RedirectToAction("Apartar");
        }

        [HttpGet]
        public ActionResult Apartar()
        {
            var boletos = TempData["BoletosSeleccionados"] as List<long>;

            if (boletos == null || !boletos.Any())
                return RedirectToAction("Index");

            ViewBag.Boletos = boletos;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmarApartado(string nombre, string dni, string whatsapp, string ids)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(whatsapp) || string.IsNullOrWhiteSpace(ids))
            {
                return RedirectToAction("Index");
            }

            var boletoIds = ids.Split(',').Select(long.Parse).ToList();

            // Obtener boletos completos desde la API para obtener FechaApartado y CodigoBoleto
            List<Boleto> boletosSeleccionados = new List<Boleto>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var todosLosBoletos = JsonConvert.DeserializeObject<List<Boleto>>(json);
                    boletosSeleccionados = todosLosBoletos.Where(b => boletoIds.Contains(b.IdBoleto)).ToList();
                }
                else
                {
                    TempData["Error"] = "No se pudo obtener la lista de boletos desde la API.";
                    return RedirectToAction("Index");
                }
            }

            // Asumamos que tienes el usuario en sesión, con Id como long
            dynamic user = Session["User"];
            if (user == null)
            {
                TempData["Error"] = "Debe iniciar sesión para apartar boletos.";
                return RedirectToAction("Index");
            }

            int userId = Convert.ToInt32(user.Id);  // Conversión explícita, asegúrate que user.Id existe y es convertible

            using (HttpClient client = new HttpClient())
            {
                foreach (var boleto in boletosSeleccionados)
                {
                    // Registrar apartado
                    var apartadoPayload = new
                    {
                        boleto = boleto.IdBoleto,
                        fechaapartados = boleto.FechaApartado ?? DateTime.Now,
                        boletoId = boleto.IdBoleto
                    };

                    var apartadoJson = JsonConvert.SerializeObject(apartadoPayload);
                    var apartadoContent = new StringContent(apartadoJson, Encoding.UTF8, "application/json");

                    var apartadoResponse = await client.PostAsync(apiApartadoUrl, apartadoContent);

                    if (!apartadoResponse.IsSuccessStatusCode)
                    {
                        TempData["Error"] = $"Error al registrar apartado para boleto {boleto.CodigoBoleto}";
                        continue; // Sigue con los demás boletos
                    }

                    // Registrar participante (sin duplicados)
                    // Primero verificar si existe participante con mismo nombre y teléfono y boletoId
                    var participanteCheckUrl = $"https://localhost:44346/api-sorteo/participantes/check?nombre={Uri.EscapeDataString(nombre)}&telefono={Uri.EscapeDataString(whatsapp)}&boletoId={boleto.IdBoleto}";
                    // Esta ruta es solo sugerencia. Si no tienes un endpoint para verificar, considera agregarlo o controlar por otro medio.

                    // Para el ejemplo, omitimos la verificación y solo enviamos el registro:

                    var participantePayload = new
                    {
                        nombre = nombre,
                        telefono = whatsapp,
                        userId = userId,
                        boletoId = boleto.IdBoleto
                    };

                    var participanteJson = JsonConvert.SerializeObject(participantePayload);
                    var participanteContent = new StringContent(participanteJson, Encoding.UTF8, "application/json");

                    var participanteResponse = await client.PostAsync(apiParticipanteUrl, participanteContent);

                    if (!participanteResponse.IsSuccessStatusCode)
                    {
                        TempData["Error"] = $"Error al registrar participante para boleto {boleto.CodigoBoleto}";
                        // Puedes hacer rollback o continuar según necesidad
                    }
                }
            }

            string boletoTexto = string.Join("%0A", boletosSeleccionados.Select(b => "🍀 BOLETO: " + b.CodigoBoleto.PadLeft(5, '0')));

            string mensaje =
                $"Hola, aparté boletos de la rifa!!%0A" +
                $"Moto Honda 2025 🚀%0A" +
                $"———————————————%0A" +
                $"{boletoTexto}%0A" +
                $"🎟️ Nombre: {nombre}%0A" +
                $"Celular: {whatsapp}%0A" +
                $"El siguiente paso es enviar el comprobante de pago por aquí.";

            string telefonoDestino = "51969736688";
            string urlWhatsApp = $"https://api.whatsapp.com/send?phone={telefonoDestino}&text={mensaje}";

            return Redirect(urlWhatsApp);
        }
    }
}