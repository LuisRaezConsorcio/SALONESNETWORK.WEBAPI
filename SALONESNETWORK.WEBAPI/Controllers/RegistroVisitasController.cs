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

        // GET: api/RegistroVisita/5
        [HttpGet("GetRegistroVisitaByUserId")]
        public async Task<ActionResult<RegistroVisita>> GetRegistroVisitaByUserId(RegistroVisita modelo)
        {
            try
            {
                var registroVisita = await _registroVisitaService.ObtenerPorIdUsuario(modelo);

                if (registroVisita == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El registro de visita no fue encontrado.", Resultado = false });
                }

                var registroVisitaDTO = new RegistroVisitaDTO
                {
                    Id = registroVisita.Id,
                    Id_Usuario = registroVisita.Id_Usuario,
                    Fecha = registroVisita.Fecha,
                    Ip = registroVisita.Ip
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro de visita encontrado.", Datos = registroVisitaDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el registro de visita.", Error = ex.Message });
            }
        }

        
        // POST: api/RegistroVisita
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostRegistroVisita")]
        public async Task<IActionResult> PostRegistroVisita(RegistroVisitaDTO modelo)
        {
            try
            {
                RegistroVisita nuevoModelo = new RegistroVisita
                {
                    Id_Usuario = modelo.Id_Usuario,
                    Fecha = DateTime.Now,
                    Ip = modelo.Ip
                };

                bool respuesta = await _registroVisitaService.Insertar(nuevoModelo);

                if (!respuesta)
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo registrar la visita.", Resultado = false });

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Registro de visita creado con éxito.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al registrar la visita.", Error = ex.Message });
            }

        }

        // DELETE: api/RegistroVisita/5
        [HttpDelete("DeleteRegistroVisita")]
        public async Task<IActionResult> DeleteRegistroVisita(RegistroVisitaDTO modelo)
        {
            try
            {
                RegistroVisita nuevoModelo = new RegistroVisita()
                {
                    Id_Usuario = modelo.Id_Usuario,
                };
                var registroVisita = await _registroVisitaService.ObtenerPorIdUsuario(nuevoModelo);

                if (registroVisita == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El registro de visita no fue encontrado.", Resultado = false });


                bool respuesta = await _registroVisitaService.Eliminar(registroVisita.Id);

                if (!respuesta)
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el registro de visita.", Resultado = false });


                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro de visita eliminado con éxito.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el registro de visita.", Error = ex.Message });
            }
        }

    }
}
