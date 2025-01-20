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
    public class AsuntoController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IAsuntoService _asuntoService;

        public AsuntoController(IAsuntoService asuntoService)
        {
            _asuntoService = asuntoService;
        }

        // GET: api/Asunto
        [HttpGet("GetAsuntos")]
        public async Task<ActionResult<IEnumerable<Asunto>>> GetAsuntos()
        {
            //return await _context.Asuntoes.ToListAsync();
            IQueryable<Asunto> queryContactoSQL = await _asuntoService.ObtenerTodos();

            List<AsuntoDTO> lista = queryContactoSQL
                                                     .Select(c => new AsuntoDTO()
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

        // GET: api/Asunto/5
        [HttpGet("GetAsuntoById")]
        public async Task<ActionResult<Asunto>> GetAsuntoById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Asunto = await _asuntoService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Asunto == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var AsuntoDTO = new AsuntoDTO
            {
                Id = Asunto.Id,
                Nombre = Asunto.Nombre,
                Descripcion = Asunto.Descripcion,
                FechaCreacion = Asunto.FechaCreacion,
                UsuarioCreacion = Asunto.UsuarioCreacion,
                FechaModificacion = Asunto.FechaModificacion,
                UsuarioModificacion = Asunto.UsuarioModificacion,
                Estado = Asunto.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(AsuntoDTO);
        }

        // PUT: api/Asunto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutAsunto")]
        public async Task<IActionResult> PutAsunto(AsuntoDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var AsuntoExistente = await _asuntoService.ObtenerPorId(modelo.Id);

            if (AsuntoExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            AsuntoExistente.Nombre = modelo.Nombre ?? AsuntoExistente.Nombre;
            AsuntoExistente.Descripcion = modelo.Descripcion ?? AsuntoExistente.Descripcion;
            AsuntoExistente.FechaModificacion = DateTime.Now;
            AsuntoExistente.UsuarioModificacion = 1;
            AsuntoExistente.Estado = modelo.Estado ?? AsuntoExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _asuntoService.Actualizar(AsuntoExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Asunto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostAsunto")]
        public async Task<IActionResult> PostAsunto(AsuntoDTO modelo)
        {

            Asunto NuevoModelo = new Asunto()
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _asuntoService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Asunto/5
        [HttpDelete("DeleteAsunto")]
        public async Task<IActionResult> DeleteAsunto(int id)
        {
            var Asunto = await _asuntoService.ObtenerPorId(id);
            if (Asunto == null)
            {
                return NotFound();
            }

            await _asuntoService.Eliminar(id);
            //await _asuntoService.SaveChangesAsync();

            return NoContent();
        }
    }
}
