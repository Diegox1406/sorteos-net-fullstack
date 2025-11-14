using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System;
using System.Web.Http;

namespace pe.com.ciberelectrik.api.Controllers
{
    [RoutePrefix("api-sorteo/boleto")]
    public class BoletoController : ApiController
    {
        private readonly BoletoRepository repositorio = new BoletoRepository();

        // GET api-sorteo/boleto
        [HttpGet]
        [Route("")]
        public IHttpActionResult findAll()
        {
            var boletos = repositorio.findAll();
            return Ok(boletos);
        }

        // GET api-sorteo/boleto/{id}
        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult findById(long id)
        {
            var boleto = repositorio.findById(id);
            if (boleto == null)
                return NotFound();
            return Ok(boleto);
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] Boleto boleto)
        {
            if (boleto == null)
                return BadRequest("Datos inválidos.");

            try
            {
                var resultado = repositorio.add(boleto);
                if (resultado)
                    return Ok("Boleto registrado correctamente.");
                else
                    return InternalServerError(new Exception("No se pudo registrar el boleto."));
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error si tienes un logger, por ejemplo:
                // _logger.LogError(ex, "Error al registrar boleto");

                // Retorna el detalle del error en la respuesta (solo recomendable en desarrollo)
                return InternalServerError(ex);
            }
        }

        // PUT api-sorteo/boleto/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update([FromBody] Boleto boleto)
        {
            if (boleto == null || boleto.IdBoleto <= 0)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.update(boleto);
            if (resultado)
                return Ok("Boleto actualizado correctamente.");
            else
                return InternalServerError();
        }

        // PUT api-sorteo/boleto/delete/{id}
        [HttpPut]
        [Route("delete/{id:long}")]
        public IHttpActionResult Delete(long id)
        {
            var resultado = repositorio.delete(id);
            if (resultado)
                return Ok("Boleto desactivado.");
            else
                return InternalServerError();
        }

        // PUT api-sorteo/boleto/enable/{id:long}
        [HttpPut]
        [Route("enable/{id:long}")]
        public IHttpActionResult Enable(long id)
        {
            var resultado = repositorio.enable(id);
            if (resultado)
                return Ok("Boleto activado.");
            else
                return InternalServerError();
        }
    }
}
