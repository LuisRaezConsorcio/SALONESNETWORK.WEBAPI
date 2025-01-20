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
    public class UbicacionMensajeController : ControllerBase
    {
        private readonly IUbicacionMensajeService _ubicacionMensajeService;

        public UbicacionMensajeController(IUbicacionMensajeService ubicacionMensajeService)
        {
            _ubicacionMensajeService = ubicacionMensajeService;
        }

        // GET: api/UbicacionMensaje
        [HttpGet("GetUbicacionMensaje")]
        public async Task<ActionResult<IEnumerable<UbicacionMensaje>>> GetUbicacionMensaje()
        {
            //return await _context.UbicacionMensaje.ToListAsync();
            IQueryable<UbicacionMensaje> queryContactoSQL = await _ubicacionMensajeService.ObtenerTodos();

            List<UbicacionMensajeDTO> lista = queryContactoSQL
                                                     .Select(c => new UbicacionMensajeDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Mensaje = c.Id_Mensaje,
                                                         Id_Asunto = c.Id_Asunto,
                                                         Id_Pais = c.Id_Pais,
                                                         Id_Seccion = c.Id_Seccion,
                                                         Id_SubSeccion = c.Id_SubSeccion
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/UbicacionMensaje/5
        [HttpGet("GetUbicacionMensajeById")]
        public async Task<ActionResult<UbicacionMensaje>> GetUbicacionMensajeById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var UbicacionMensaje = await _ubicacionMensajeService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (UbicacionMensaje == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var UbicacionMensajeDTO = new UbicacionMensajeDTO
            {
                Id = UbicacionMensaje.Id,
                Id_Mensaje = UbicacionMensaje.Id_Mensaje,
                Id_Asunto = UbicacionMensaje.Id_Asunto,
                Id_Pais = UbicacionMensaje.Id_Pais,
                Id_Seccion = UbicacionMensaje.Id_Seccion,
                Id_SubSeccion = UbicacionMensaje.Id_SubSeccion
            };

            // Retorna el DTO con un status 200
            return Ok(UbicacionMensajeDTO);
        }

        // PUT: api/UbicacionMensaje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUbicacionMensaje")]
        public async Task<IActionResult> PutUbicacionMensaje(UbicacionMensajeDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var UbicacionMensajeExistente = await _ubicacionMensajeService.ObtenerPorId(modelo.Id);

            if (UbicacionMensajeExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            UbicacionMensajeExistente.Id_Mensaje = modelo.Id_Mensaje ?? UbicacionMensajeExistente.Id_Mensaje;
            UbicacionMensajeExistente.Id_Asunto = modelo.Id_Asunto ?? UbicacionMensajeExistente.Id_Asunto;
            UbicacionMensajeExistente.Id_Pais = modelo.Id_Pais ?? UbicacionMensajeExistente.Id_Pais;
            UbicacionMensajeExistente.Id_Seccion = modelo.Id_Seccion ?? UbicacionMensajeExistente.Id_Seccion;
            UbicacionMensajeExistente.Id_SubSeccion = modelo.Id_SubSeccion ?? UbicacionMensajeExistente.Id_SubSeccion;

            // Realizar la actualización
            bool respuesta = await _ubicacionMensajeService.Actualizar(UbicacionMensajeExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/UbicacionMensaje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUbicacionMensaje")]
        public async Task<IActionResult> PostUbicacionMensaje(UbicacionMensajeDTO modelo)
        {

            UbicacionMensaje NuevoModelo = new UbicacionMensaje()
            {
                Id_Mensaje = modelo.Id_Mensaje,
                Id_Asunto = modelo.Id_Asunto,
                Id_Pais = modelo.Id_Pais,
                Id_Seccion = modelo.Id_Seccion,
                Id_SubSeccion = modelo.Id_SubSeccion
            };

            bool respuesta = await _ubicacionMensajeService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/UbicacionMensaje/5
        [HttpDelete("DeleteUbicacionMensaje")]
        public async Task<IActionResult> DeleteUbicacionMensaje(int id)
        {
            var UbicacionMensaje = await _ubicacionMensajeService.ObtenerPorId(id);
            if (UbicacionMensaje == null)
            {
                return NotFound();
            }

            await _ubicacionMensajeService.Eliminar(id);
            //await _ubicacionMensajeService.SaveChangesAsync();

            return NoContent();
        }
    }
}
