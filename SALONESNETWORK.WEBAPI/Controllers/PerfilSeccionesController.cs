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
    public class PerfilSeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IPerfilSeccionService _perfilSeccionService;

        public PerfilSeccionesController(IPerfilSeccionService perfilSeccionService)
        {
            _perfilSeccionService = perfilSeccionService;
        }

        // GET: api/PerfilSeccion
        [HttpGet("GetPerfilSecciones")]
        public async Task<ActionResult<IEnumerable<PerfilSeccion>>> GetPerfilSecciones()
        {
            //return await _context.PerfilSecciones.ToListAsync();
            IQueryable<PerfilSeccion> queryContactoSQL = await _perfilSeccionService.ObtenerTodos();

            List<PerfilSeccionDTO> lista = queryContactoSQL
                                                     .Select(c => new PerfilSeccionDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Seccion = c.Id_Seccion,
                                                         Id_Perfil = c.Id_Perfil
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/PerfilSeccion/5
        [HttpGet("GetPerfilSeccionById")]
        public async Task<ActionResult<PerfilSeccion>> GetPerfilSeccionById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var PerfilSeccion = await _perfilSeccionService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (PerfilSeccion == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var PerfilSeccionDTO = new PerfilSeccionDTO
            {
                Id = PerfilSeccion.Id,
                Id_Seccion = PerfilSeccion.Id_Seccion,
                Id_Perfil = PerfilSeccion.Id_Perfil
            };

            // Retorna el DTO con un status 200
            return Ok(PerfilSeccionDTO);
        }

        // PUT: api/PerfilSeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutPerfilSeccion")]
        public async Task<IActionResult> PutPerfilSeccion(PerfilSeccionDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var PerfilSeccionExistente = await _perfilSeccionService.ObtenerPorId(modelo.Id);

            if (PerfilSeccionExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            PerfilSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? PerfilSeccionExistente.Id_Seccion;
            PerfilSeccionExistente.Id_Perfil = modelo.Id_Perfil ?? PerfilSeccionExistente.Id_Perfil;

            // Realizar la actualización
            bool respuesta = await _perfilSeccionService.Actualizar(PerfilSeccionExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/PerfilSeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostPerfilSeccion")]
        public async Task<IActionResult> PostPerfilSeccion(PerfilSeccionDTO modelo)
        {

            PerfilSeccion NuevoModelo = new PerfilSeccion()
            {
                Id_Seccion = modelo.Id_Seccion,
                Id_Perfil = modelo.Id_Perfil
            };

            bool respuesta = await _perfilSeccionService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/PerfilSeccion/5
        [HttpDelete("DeletePerfilSeccion")]
        public async Task<IActionResult> DeletePerfilSeccion(int id)
        {
            var PerfilSeccion = await _perfilSeccionService.ObtenerPorId(id);
            if (PerfilSeccion == null)
            {
                return NotFound();
            }

            await _perfilSeccionService.Eliminar(id);
            //await _PerfilSeccionService.SaveChangesAsync();

            return NoContent();
        }

        //private bool PerfilSeccionExists(int id)
        //{
        //    return _context.PerfilSecciones.Any(e => e.Id == id);
        //}
    }
}
