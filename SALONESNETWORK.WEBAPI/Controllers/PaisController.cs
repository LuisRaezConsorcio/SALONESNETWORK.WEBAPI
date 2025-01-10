using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
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
    public class PaisController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        // GET: api/Pais
        [HttpGet("GetPaises")]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            //return await _context.Paises.ToListAsync();
            IQueryable<Pais> queryContactoSQL = await _paisService.ObtenerTodos();

            List<PaisDTO> lista = queryContactoSQL
                                                     .Select(c => new PaisDTO()
                                                     {
                                                         Id = c.Id,
                                                         Nombre = c.Nombre,
                                                         FechaCreacion = c.FechaCreacion,
                                                         UsuarioCreacion = c.UsuarioCreacion,
                                                         FechaModificacion = c.FechaModificacion,
                                                         UsuarioModificacion = c.UsuarioModificacion,
                                                         Estado = c.Estado,
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Pais/5
        [HttpGet("GetPaisById")]
        public async Task<ActionResult<Pais>> GetPaisById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var pais = await _paisService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (pais == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var paisDTO = new PaisDTO
            {
                Id = pais.Id,
                Nombre = pais.Nombre,
                FechaCreacion = pais.FechaCreacion,
                UsuarioCreacion = pais.UsuarioCreacion,
                FechaModificacion = pais.FechaModificacion,
                UsuarioModificacion = pais.UsuarioModificacion,
                Estado = pais.Estado
            };

            // Retorna el DTO con un status 200
            return Ok(paisDTO);
        }

        // PUT: api/Pais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutPais")]
        public async Task<IActionResult> PutPais(PaisDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var paisExistente = await _paisService.ObtenerPorId(modelo.Id);

            if (paisExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            paisExistente.Nombre = modelo.Nombre ?? paisExistente.Nombre;
            paisExistente.FechaModificacion = DateTime.Now;
            paisExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? paisExistente.UsuarioModificacion;
            paisExistente.Estado = modelo.Estado ?? paisExistente.Estado;

            // Realizar la actualización
            bool respuesta = await _paisService.Actualizar(paisExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Pais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostPais")]
        public async Task<IActionResult> PostPais(PaisDTO modelo)
        {

            Pais NuevoModelo = new Pais()
            {
                Nombre = modelo.Nombre,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Estado = true
            };

            bool respuesta = await _paisService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Pais/5
        [HttpDelete("DeletePais")]
        public async Task<IActionResult> DeletePais(int id)
        {
            var pais = await _paisService.ObtenerPorId(id);
            if (pais == null)
            {
                return NotFound();
            }

            await _paisService.Eliminar(id);
            //await _paisService.SaveChangesAsync();

            return NoContent();
        }

        //private bool PaisExists(int id)
        //{
        //    return _context.Paises.Any(e => e.Id == id);
        //}
    }
}
