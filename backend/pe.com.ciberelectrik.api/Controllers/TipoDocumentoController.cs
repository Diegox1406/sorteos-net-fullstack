using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System.Web.Http;

namespace pe.com.ciberelectrik.api.Controllers
{
    [Route("api-ciberelectrik/tipodocumento")]
    public class TipoDocumentoController : ApiController
    {
        private readonly TipoDocumentoRepository repositorio = new TipoDocumentoRepository();

        // GET: api-ciberelectrik/tipodocumento
        [HttpGet]
        public IHttpActionResult findAll()
        {
            var tiposDocumento = repositorio.findAll();
            return Ok(tiposDocumento);
        }


        [HttpGet]
        [Route("api-ciberelectrik/tipodocumento/custom")]
        public IHttpActionResult findAllCustom()
        {
            var tipos = repositorio.findAllCustom();
            return Ok(tipos);
        }

        // POST: api-ciberelectrik/tipodocumento/register
        [HttpPost]
        [Route("api-ciberelectrik/tipodocumento/register")]
        public IHttpActionResult Register([FromBody] TipoDocumento tipoDocumento)
        {
            if (tipoDocumento == null)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.add(tipoDocumento);
            if (resultado)
                return Ok("TipoDocumento registrado correctamente.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/tipodocumento/update
        [HttpPut]
        [Route("api-ciberelectrik/tipodocumento/update")]
        public IHttpActionResult Update([FromBody] TipoDocumento tipoDocumento)
        {
            if (tipoDocumento == null || tipoDocumento.codigo <= 0)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.update(tipoDocumento);
            if (resultado)
                return Ok("TipoDocumento actualizado correctamente.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/tipodocumento/delete/{id}
        [HttpPut]
        [Route("api-ciberelectrik/tipodocumento/delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var tipoDocumento = new TipoDocumento { codigo = id };
            var resultado = repositorio.delete(tipoDocumento);
            if (resultado)
                return Ok("TipoDocumento desactivado.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/tipodocumento/enable/{id}
        [HttpPut]
        [Route("api-ciberelectrik/tipodocumento/enable/{id:int}")]
        public IHttpActionResult Enable(int id)
        {
            var tipoDocumento = new TipoDocumento { codigo = id };
            var resultado = repositorio.enable(tipoDocumento);
            if (resultado)
                return Ok("TipoDocumento activado.");
            else
                return InternalServerError();
        }
    }
}
