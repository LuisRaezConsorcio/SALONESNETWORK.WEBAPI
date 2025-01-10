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
    public class SeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly ISeccionService _seccioneService;

        public SeccionesController(ISeccionService seccioneService)
        {
            _seccioneService = seccioneService;
        }

        // GET: api/Seccione
        [HttpGet("GetSecciones")]
        public async Task<ActionResult<IEnumerable<Seccion>>> GetSecciones()
        {
            //return await _context.Seccionees.ToListAsync();
            IQueryable<Seccion> queryContactoSQL = await _seccioneService.ObtenerTodos();

            List<SeccionDTO> lista = queryContactoSQL
                                                     .Select(c => new SeccionDTO()
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

        // GET: api/Seccione/5
        [HttpGet("GetSeccionById")]
        public async Task<ActionResult<Seccion>> GetSeccionById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Seccione = await _seccioneService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Seccione == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var SeccioneDTO = new SeccionDTO
            {
                Id = Seccione.Id,
                Nombre = Seccione.Nombre,
                Descripcion = Seccione.Descripcion,
                FechaCreacion = Seccione.FechaCreacion,
                UsuarioCreacion = Seccione.UsuarioCreacion,
                FechaModificacion = Seccione.FechaModificacion,
                UsuarioModificacion = Seccione.UsuarioModificacion,
                Estado = Seccione.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(SeccioneDTO);
        }

        // PUT: api/Seccione/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutSeccion")]
        public async Task<IActionResult> PutSeccion(SeccionDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var SeccioneExistente = await _seccioneService.ObtenerPorId(modelo.Id);

            if (SeccioneExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            SeccioneExistente.Nombre = modelo.Nombre ?? SeccioneExistente.Nombre;
            SeccioneExistente.Descripcion = modelo.Descripcion ?? SeccioneExistente.Descripcion;
            SeccioneExistente.FechaModificacion = DateTime.Now;
            SeccioneExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? SeccioneExistente.UsuarioModificacion;
            SeccioneExistente.Estado = modelo.Estado ?? SeccioneExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _seccioneService.Actualizar(SeccioneExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Seccione
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostSeccion")]
        public async Task<IActionResult> PostSeccion(SeccionDTO modelo)
        {

            Seccion NuevoModelo = new Seccion()
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _seccioneService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Seccione/5
        [HttpDelete("DeleteSeccion")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            var Seccione = await _seccioneService.ObtenerPorId(id);
            if (Seccione == null)
            {
                return NotFound();
            }

            await _seccioneService.Eliminar(id);
            //await _seccioneService.SaveChangesAsync();

            return NoContent();
        }

        //private bool SeccioneExists(int id)
        //{
        //    return _context.Seccionees.Any(e => e.Id == id);
        //}
    }
}
