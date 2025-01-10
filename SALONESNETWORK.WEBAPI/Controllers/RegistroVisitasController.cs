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
    public class RegistroVisitasController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IRegistroVisitaService _registroVisitaService;

        public RegistroVisitasController(IRegistroVisitaService registroVisitaService)
        {
            _registroVisitaService = registroVisitaService;
        }

        // GET: api/RegistroVisita
        [HttpGet("GetRegistroVisitas")]
        public async Task<ActionResult<IEnumerable<RegistroVisita>>> GetRegistroVisitas()
        {
            //return await _context.RegistroVisitaes.ToListAsync();
            IQueryable<RegistroVisita> queryContactoSQL = await _registroVisitaService.ObtenerTodos();

            List<RegistroVisitaDTO> lista = queryContactoSQL
                                                     .Select(c => new RegistroVisitaDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Usuario = c.Id_Usuario,
                                                         Fecha = c.Fecha,
                                                         Ip = c.Ip
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/RegistroVisita/5
        [HttpGet("GetRegistroVisitaById")]
        public async Task<ActionResult<RegistroVisita>> GetRegistroVisitaById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var RegistroVisita = await _registroVisitaService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (RegistroVisita == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var RegistroVisitaDTO = new RegistroVisitaDTO
            {
                Id = RegistroVisita.Id,
                Id_Usuario = RegistroVisita.Id_Usuario,
                Fecha = RegistroVisita.Fecha,
                Ip = RegistroVisita.Ip
            };

            // Retorna el DTO con un status 200
            return Ok(RegistroVisitaDTO);
        }

        // PUT: api/RegistroVisita/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutRegistroVisita")]
        public async Task<IActionResult> PutRegistroVisita(RegistroVisitaDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var RegistroVisitaExistente = await _registroVisitaService.ObtenerPorId(modelo.Id);

            if (RegistroVisitaExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            RegistroVisitaExistente.Id_Usuario = modelo.Id_Usuario ?? RegistroVisitaExistente.Id_Usuario;
            RegistroVisitaExistente.Fecha = modelo.Fecha ?? RegistroVisitaExistente.Fecha;
            RegistroVisitaExistente.Ip = modelo.Ip ?? RegistroVisitaExistente.Ip;

            // Realizar la actualización
            bool respuesta = await _registroVisitaService.Actualizar(RegistroVisitaExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/RegistroVisita
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostRegistroVisita")]
        public async Task<IActionResult> PostRegistroVisita(RegistroVisitaDTO modelo)
        {

            RegistroVisita NuevoModelo = new RegistroVisita()
            {
                Id_Usuario = modelo.Id_Usuario,
                Fecha = DateTime.Now,
                Ip = modelo.Ip
            };

            bool respuesta = await _registroVisitaService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/RegistroVisita/5
        [HttpDelete("DeleteRegistroVisita")]
        public async Task<IActionResult> DeleteRegistroVisita(int id)
        {
            var RegistroVisita = await _registroVisitaService.ObtenerPorId(id);
            if (RegistroVisita == null)
            {
                return NotFound();
            }

            await _registroVisitaService.Eliminar(id);
            //await _registroVisitaService.SaveChangesAsync();

            return NoContent();
        }

        //private bool RegistroVisitaExists(int id)
        //{
        //    return _context.RegistroVisitaes.Any(e => e.Id == id);
        //}
    }
}
