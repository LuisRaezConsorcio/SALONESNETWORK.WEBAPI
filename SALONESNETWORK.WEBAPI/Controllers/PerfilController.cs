using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.BLL.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET: api/Perfil
        [HttpGet("GetPerfiles")]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfiles()
        {
            //return await _context.Perfiles.ToListAsync();
            IQueryable<Perfil> queryContactoSQL = await _perfilService.ObtenerTodos();

            List<PerfilDTO> lista = queryContactoSQL
                                                     .Select(c => new PerfilDTO()
                                                     {
                                                         Id = c.Id,
                                                         Nombre = c.Nombre,
                                                         Descripcion = c.Descripcion,
                                                         FechaCreacion = c.FechaCreacion,
                                                         UsuarioCreacion = c.UsuarioCreacion,
                                                         FechaModificacion = c.FechaModificacion,
                                                         UsuarioModificacion = c.UsuarioModificacion,
                                                         Estado = c.Estado,
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Perfil/5
        [HttpGet("GetPerfilById")]
        public async Task<ActionResult<Perfil>> GetPerfilById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Perfil = await _perfilService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Perfil == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var PerfilDTO = new PerfilDTO
            {
                Id = Perfil.Id,
                Nombre = Perfil.Nombre,
                Descripcion = Perfil.Descripcion,
                FechaCreacion = Perfil.FechaCreacion,
                UsuarioCreacion = Perfil.UsuarioCreacion,
                FechaModificacion = Perfil.FechaModificacion,
                UsuarioModificacion = Perfil.UsuarioModificacion,
                Estado = Perfil.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(PerfilDTO);
        }

        // PUT: api/Perfil/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutPerfil")]
        public async Task<IActionResult> PutPerfil(PerfilDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var PerfilExistente = await _perfilService.ObtenerPorId(modelo.Id);

            if (PerfilExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            PerfilExistente.Nombre = modelo.Nombre ?? PerfilExistente.Nombre;
            PerfilExistente.Descripcion = modelo.Descripcion ?? PerfilExistente.Descripcion;
            PerfilExistente.FechaModificacion = DateTime.Now;
            PerfilExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? PerfilExistente.UsuarioModificacion;
            PerfilExistente.Estado = modelo.Estado ?? PerfilExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _perfilService.Actualizar(PerfilExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Perfil
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostPerfil")]
        public async Task<IActionResult> PostPerfil(PerfilDTO modelo)
        {

            Perfil NuevoModelo = new Perfil()
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _perfilService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Perfil/5
        [HttpDelete("DeletePerfil")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            var Perfil = await _perfilService.ObtenerPorId(id);
            if (Perfil == null)
            {
                return NotFound();
            }

            await _perfilService.Eliminar(id);
            //await _perfilService.SaveChangesAsync();

            return NoContent();
        }

        //private bool PerfilExists(int id)
        //{
        //    return _context.Perfiles.Any(e => e.Id == id);
        //}
    }
}
