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
    public class TipoMensajesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly ITipoMensajeService _tipoMensajeService;

        public TipoMensajesController(ITipoMensajeService tipoMensajeService)
        {
            _tipoMensajeService = tipoMensajeService;
        }

        // GET: api/TipoMensaje
        [HttpGet("GetTipoMensajes")]
        public async Task<ActionResult<IEnumerable<TipoMensaje>>> GetTipoMensajes()
        {
            //return await _context.TipoMensajees.ToListAsync();
            IQueryable<TipoMensaje> queryContactoSQL = await _tipoMensajeService.ObtenerTodos();

            List<TipoMensajeDTO> lista = queryContactoSQL
                                                     .Select(c => new TipoMensajeDTO()
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

        // GET: api/TipoMensaje/5
        [HttpGet("GetTipoMensajeById")]
        public async Task<ActionResult<TipoMensaje>> GetTipoMensajeById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var TipoMensaje = await _tipoMensajeService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (TipoMensaje == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var TipoMensajeDTO = new TipoMensajeDTO
            {
                Id = TipoMensaje.Id,
                Nombre = TipoMensaje.Nombre,
                Descripcion = TipoMensaje.Descripcion,
                FechaCreacion = TipoMensaje.FechaCreacion,
                UsuarioCreacion = TipoMensaje.UsuarioCreacion,
                FechaModificacion = TipoMensaje.FechaModificacion,
                UsuarioModificacion = TipoMensaje.UsuarioModificacion,
                Estado = TipoMensaje.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(TipoMensajeDTO);
        }

        // PUT: api/TipoMensaje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutTipoMensaje")]
        public async Task<IActionResult> PutTipoMensaje(TipoMensajeDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var TipoMensajeExistente = await _tipoMensajeService.ObtenerPorId(modelo.Id);

            if (TipoMensajeExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            TipoMensajeExistente.Nombre = modelo.Nombre ?? TipoMensajeExistente.Nombre;
            TipoMensajeExistente.Descripcion = modelo.Descripcion ?? TipoMensajeExistente.Descripcion;
            TipoMensajeExistente.FechaModificacion = DateTime.Now;
            TipoMensajeExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? TipoMensajeExistente.UsuarioModificacion;
            TipoMensajeExistente.Estado = modelo.Estado ?? TipoMensajeExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _tipoMensajeService.Actualizar(TipoMensajeExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/TipoMensaje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostTipoMensaje")]
        public async Task<IActionResult> PostTipoMensaje(TipoMensajeDTO modelo)
        {

            TipoMensaje NuevoModelo = new TipoMensaje()
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _tipoMensajeService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/TipoMensaje/5
        [HttpDelete("DeleteTipoMensaje")]
        public async Task<IActionResult> DeleteTipoMensaje(int id)
        {
            var TipoMensaje = await _tipoMensajeService.ObtenerPorId(id);
            if (TipoMensaje == null)
            {
                return NotFound();
            }

            await _tipoMensajeService.Eliminar(id);
            //await _tipoMensajeService.SaveChangesAsync();

            return NoContent();
        }

        //private bool TipoMensajeExists(int id)
        //{
        //    return _context.TipoMensajees.Any(e => e.Id == id);
        //}
    }
}
