using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System;
using System.Web.Http;
using System.Net;

namespace pe.com.ciberelectrik.api.Controllers
{
    [RoutePrefix("api-sorteo/participantes")]
    public class ParticipanteController : ApiController
    {
        private readonly ParticipanteRepository repositorio = new ParticipanteRepository();

        // GET api-sorteo/participantes
        [HttpGet]
        [Route("")]
        public IHttpActionResult FindAll()
        {
            var participantes = repositorio.findAll();
            return Ok(participantes);
        }

        // GET api-sorteo/participantes/{id}
        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult FindById(long id)
        {
            var participante = repositorio.findById(id);
            if (participante == null)
                return NotFound();

            return Ok(participante);
        }

        // POST api-sorteo/participantes/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] Participante participante)
        {
            if (participante == null)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.add(participante);

            if (resultado.Success)
                return Ok(new { success = true, message = resultado.Message });
            else
                return Content(HttpStatusCode.BadRequest, new { success = false, message = resultado.Message });
        }

        // PUT api-sorteo/participantes/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update([FromBody] Participante participante)
        {
            if (participante == null || participante.IdParticipante <= 0)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.update(participante);

            if (resultado)
                return Ok(new { success = true, message = "Participante actualizado correctamente." });
            else
                return Content(HttpStatusCode.InternalServerError, new { success = false, message = "No se pudo actualizar el participante." });
        }

        // DELETE api-sorteo/participantes/delete/{id}
        [HttpDelete]
        [Route("delete/{id:long}")]
        public IHttpActionResult Delete(long id)
        {
            var resultado = repositorio.delete(id);

            if (resultado)
                return Ok(new { success = true, message = "Participante eliminado." });
            else
                return Content(HttpStatusCode.InternalServerError, new { success = false, message = "No se pudo eliminar el participante." });
        }
    }
}