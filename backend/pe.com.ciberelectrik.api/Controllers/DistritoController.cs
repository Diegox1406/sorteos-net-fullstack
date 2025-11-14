using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System.Web.Http;

namespace pe.com.ciberelectrik.api.Controllers
{
    // Ruta base para Distrito
    [RoutePrefix("api-ciberelectrik/distrito")]
    public class DistritoController : ApiController
    {
        private readonly DistritoRepository repositorio = new DistritoRepository();

        // GET: api-ciberelectrik/distrito
        [HttpGet]
        [Route("")]
        public IHttpActionResult findAll()
        {
            var distritos = repositorio.findAll();

            if (distritos == null)
                return InternalServerError(new System.Exception("La lista de distritos es null"));

            return Ok(distritos);
        }

        // GET: api-ciberelectrik/distrito/custom
        [HttpGet]
        [Route("custom")]
        public IHttpActionResult findAllCustom()
        {
            var distritos = repositorio.findAllCustom();

            if (distritos == null)
                return InternalServerError(new System.Exception("La lista de distritos (custom) es null"));

            return Ok(distritos);
        }

        // POST: api-ciberelectrik/distrito/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] Distrito distrito)
        {
            if (distrito == null)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.add(distrito);
            if (resultado)
                return Ok("Distrito registrado correctamente.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/distrito/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update([FromBody] Distrito distrito)
        {
            if (distrito == null || distrito.codigo <= 0)
                return BadRequest("Datos inválidos.");

            var resultado = repositorio.update(distrito);
            if (resultado)
                return Ok("Distrito actualizado correctamente.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/distrito/delete/{id}
        [HttpPut]
        [Route("delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var distrito = new Distrito { codigo = id };
            var resultado = repositorio.delete(distrito);
            if (resultado)
                return Ok("Distrito desactivado.");
            else
                return InternalServerError();
        }

        // PUT: api-ciberelectrik/distrito/enable/{id}
        [HttpPut]
        [Route("enable/{id:int}")]
        public IHttpActionResult Enable(int id)
        {
            var distrito = new Distrito { codigo = id };
            var resultado = repositorio.enable(distrito);
            if (resultado)
                return Ok("Distrito activado.");
            else
                return InternalServerError();
        }
    }
}