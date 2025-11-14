using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System;
using System.Web.Http;
using System.Net;

namespace pe.com.ciberelectrik.api.Controllers
{
    [RoutePrefix("api-sorteo/apartados")]
    public class ApartadosController : ApiController
    {
        private readonly ApartadosRepository repositorio = new ApartadosRepository();

        // GET api-sorteo/apartados
        [HttpGet]
        [Route("")]
        public IHttpActionResult FindAll()
        {
            var apartados = repositorio.findAll();
            return Ok(apartados);
        }

        // GET api-sorteo/apartados/{id}
        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult FindById(long id)
        {
            var apartado = repositorio.findById(id);
            if (apartado == null)
                return NotFound();

            return Ok(apartado);
        }

        // POST api-sorteo/apartados/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] Apartados apartado)
        {
            if (apartado == null)
                return BadRequest("Datos inválidos.");

            try
            {
                var resultado = repositorio.add(apartado);

                if (resultado.Success)
                    return Ok(new { success = true, message = resultado.Message });
                else
                    return Content(HttpStatusCode.BadRequest, new { success = false, message = resultado.Message });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // PUT api-sorteo/apartados/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update([FromBody] Apartados apartado)
        {
            if (apartado == null || apartado.IdApartados <= 0)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.update(apartado);

            if (resultado)
                return Ok(new { success = true, message = "Apartado actualizado correctamente." });
            else
                return Content(HttpStatusCode.InternalServerError, new { success = false, message = "No se pudo actualizar el apartado." });
        }

        // PUT api-sorteo/apartados/delete/{id}
        [HttpPut]
        [Route("delete/{id:long}")]
        public IHttpActionResult Delete(long id)
        {
            var resultado = repositorio.delete(id);

            if (resultado)
                return Ok(new { success = true, message = "Apartado desactivado." });
            else
                return Content(HttpStatusCode.InternalServerError, new { success = false, message = "No se pudo desactivar el apartado." });
        }
    }
}