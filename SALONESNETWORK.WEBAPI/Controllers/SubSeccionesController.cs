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
    public class SubSubSeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly ISubSeccionService _subSeccioneService;

        public SubSubSeccionesController(ISubSeccionService subSeccioneService)
        {
            _subSeccioneService = subSeccioneService;
        }

        // GET: api/SubSeccione
        [HttpGet("GetSubSecciones")]
        public async Task<ActionResult<IEnumerable<SubSeccion>>> GetSubSecciones()
        {
            //return await _context.SubSeccionees.ToListAsync();
            IQueryable<SubSeccion> queryContactoSQL = await _subSeccioneService.ObtenerTodos();

            List<SubSeccionDTO> lista = queryContactoSQL
                                                     .Select(c => new SubSeccionDTO()
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

        // GET: api/SubSeccione/5
        [HttpGet("GetSubSeccionById")]
        public async Task<ActionResult<SubSeccion>> GetSubSeccionById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var SubSeccione = await _subSeccioneService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (SubSeccione == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var SubSeccioneDTO = new SubSeccionDTO
            {
                Id = SubSeccione.Id,
                Nombre = SubSeccione.Nombre,
                Descripcion = SubSeccione.Descripcion,
                FechaCreacion = SubSeccione.FechaCreacion,
                UsuarioCreacion = SubSeccione.UsuarioCreacion,
                FechaModificacion = SubSeccione.FechaModificacion,
                UsuarioModificacion = SubSeccione.UsuarioModificacion,
                Estado = SubSeccione.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(SubSeccioneDTO);
        }

        // PUT: api/SubSeccione/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutSubSeccion")]
        public async Task<IActionResult> PutSubSeccion(SubSeccionDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var SubSeccioneExistente = await _subSeccioneService.ObtenerPorId(modelo.Id);

            if (SubSeccioneExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            SubSeccioneExistente.Nombre = modelo.Nombre ?? SubSeccioneExistente.Nombre;
            SubSeccioneExistente.Descripcion = modelo.Descripcion ?? SubSeccioneExistente.Descripcion;
            SubSeccioneExistente.FechaModificacion = DateTime.Now;
            SubSeccioneExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? SubSeccioneExistente.UsuarioModificacion;
            SubSeccioneExistente.Estado = modelo.Estado ?? SubSeccioneExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _subSeccioneService.Actualizar(SubSeccioneExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/SubSeccione
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostSubSeccion")]
        public async Task<IActionResult> PostSubSeccion(SubSeccionDTO modelo)
        {

            SubSeccion NuevoModelo = new SubSeccion()
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _subSeccioneService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/SubSeccione/5
        [HttpDelete("DeleteSubSeccion")]
        public async Task<IActionResult> DeleteSubSeccion(int id)
        {
            var SubSeccione = await _subSeccioneService.ObtenerPorId(id);
            if (SubSeccione == null)
            {
                return NotFound();
            }

            await _subSeccioneService.Eliminar(id);
            //await _subSeccioneService.SaveChangesAsync();

            return NoContent();
        }

        //private bool SubSeccioneExists(int id)
        //{
        //    return _context.SubSeccionees.Any(e => e.Id == id);
        //}
    }
}
