using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.repository;
using System;
using System.Web.Http;

namespace pe.com.ciberelectrik.api.Controllers
{
    [RoutePrefix("api-sorteo/users")]
    public class UsersController : ApiController
    {
        private readonly UsersRepository repo = new UsersRepository();

        // GET: api-sorteo/users
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var usuarios = repo.findAll();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // POST: api-sorteo/users/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] Users user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest("Email, username y password son obligatorios.");

            try
            {
                bool resultado = repo.add(user);
                if (resultado)
                    return Ok("Usuario registrado correctamente.");
                else
                    return BadRequest("No se pudo registrar el usuario.");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // PUT: api-sorteo/users/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update([FromBody] Users user)
        {
            if (user == null || user.Id <= 0)
                return BadRequest("Datos inválidos.");

            try
            {
                bool resultado = repo.update(user);
                if (resultado)
                    return Ok("Usuario actualizado correctamente.");
                else
                    return BadRequest("No se pudo actualizar el usuario.");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // DELETE: api-sorteo/users/{id}
        [HttpDelete]
        [Route("{id:long}")]
        public IHttpActionResult Delete(long id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            try
            {
                bool resultado = repo.delete(id);
                if (resultado)
                    return Ok("Usuario eliminado correctamente.");
                else
                    return BadRequest("No se pudo eliminar el usuario.");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }
    }
}
