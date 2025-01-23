using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.MODELS.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;
using SALONESNETWORK.BLL.Helpers;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroVisitasController : ControllerBase
    {

        private readonly IRegistroVisitaService _registroVisitaService;

        public RegistroVisitasController(IRegistroVisitaService registroVisitaService)
        {
            _registroVisitaService = registroVisitaService;
        }

        // GET: api/RegistroVisita/5
        [HttpGet("GetRegistroVisitaByUserId")]
        public async Task<IActionResult> GetRegistroVisitaByUserId(RegistroVisita modelo)
        {
            try
            {
                var registroVisita = await _registroVisitaService.ObtenerPorIdUsuario(modelo);

                if (registroVisita == null)
                {
                    return ResponseHelper.NotFoundResponse("El registro de visita no fue encontrado.");
                }

                var registroVisitaDTO = new RegistroVisitaDTO
                {
                    Id = registroVisita.Id,
                    Id_Usuario = registroVisita.Id_Usuario,
                    Fecha = registroVisita.Fecha,
                    Ip = registroVisita.Ip
                };

                return ResponseHelper.Success(registroVisitaDTO, "Registro de visita obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo registrar la visita.");

                return ResponseHelper.Success("Registro de visita creado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El registro de visita no fue encontrado.");


                bool respuesta = await _registroVisitaService.Eliminar(registroVisita.Id);

                if (!respuesta)
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el registro de visita.");


                return ResponseHelper.Success("Registro de visita eliminado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

    }
}
