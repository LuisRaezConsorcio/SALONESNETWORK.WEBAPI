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
    public class UsuarioSeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IUsuarioSeccionService _UsuarioSeccionService;

        public UsuarioSeccionesController(IUsuarioSeccionService UsuarioSeccionService)
        {
            _UsuarioSeccionService = UsuarioSeccionService;
        }

        // GET: api/UsuarioSeccion
        [HttpGet("GetUsuarioSecciones")]
        public async Task<ActionResult<IEnumerable<UsuarioSeccion>>> GetUsuarioSecciones()
        {
            //return await _context.UsuarioSecciones.ToListAsync();
            IQueryable<UsuarioSeccion> queryContactoSQL = await _UsuarioSeccionService.ObtenerTodos();

            List<UsuarioSeccionDTO> lista = queryContactoSQL
                                                     .Select(c => new UsuarioSeccionDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Usuario = c.Id_Usuario,
                                                         Id_Seccion = c.Id_Seccion
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/UsuarioSeccion/5
        [HttpGet("GetUsuarioSeccionById")]
        public async Task<ActionResult<UsuarioSeccion>> GetUsuarioSeccionById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var UsuarioSeccion = await _UsuarioSeccionService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (UsuarioSeccion == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var UsuarioSeccionDTO = new UsuarioSeccionDTO
            {
                Id = UsuarioSeccion.Id,
                Id_Usuario = UsuarioSeccion.Id_Usuario,
                Id_Seccion = UsuarioSeccion.Id_Seccion
            };

            // Retorna el DTO con un status 200
            return Ok(UsuarioSeccionDTO);
        }

        // PUT: api/UsuarioSeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUsuarioSeccion")]
        public async Task<IActionResult> PutUsuarioSeccion(UsuarioSeccionDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var UsuarioSeccionExistente = await _UsuarioSeccionService.ObtenerPorId(modelo.Id);

            if (UsuarioSeccionExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            UsuarioSeccionExistente.Id_Usuario = modelo.Id_Usuario ?? UsuarioSeccionExistente.Id_Usuario;
            UsuarioSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? UsuarioSeccionExistente.Id_Seccion;

            // Realizar la actualización
            bool respuesta = await _UsuarioSeccionService.Actualizar(UsuarioSeccionExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/UsuarioSeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUsuarioSeccion")]
        public async Task<IActionResult> PostUsuarioSeccion(UsuarioSeccionDTO modelo)
        {

            UsuarioSeccion NuevoModelo = new UsuarioSeccion()
            {
                Id_Usuario = modelo.Id_Usuario,
                Id_Seccion = modelo.Id_Seccion
            };

            bool respuesta = await _UsuarioSeccionService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/UsuarioSeccion/5
        [HttpDelete("DeleteUsuarioSeccion")]
        public async Task<IActionResult> DeleteUsuarioSeccion(int id)
        {
            var UsuarioSeccion = await _UsuarioSeccionService.ObtenerPorId(id);
            if (UsuarioSeccion == null)
            {
                return NotFound();
            }

            await _UsuarioSeccionService.Eliminar(id);
            //await _UsuarioSeccionService.SaveChangesAsync();

            return NoContent();
        }

        //private bool UsuarioSeccionExists(int id)
        //{
        //    return _context.UsuarioSecciones.Any(e => e.Id == id);
        //}
    }
}
