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
    public class UbicacionMensajeController : ControllerBase
    {
        private readonly IUbicacionMensajeService _ubicacionMensajeService;

        public UbicacionMensajeController(IUbicacionMensajeService ubicacionMensajeService)
        {
            _ubicacionMensajeService = ubicacionMensajeService;
        }

        // GET: api/UbicacionMensaje
        [HttpGet("GetUbicacionMensaje")]
        public async Task<IActionResult> GetUbicacionMensaje()
        {
            try
            {
                IQueryable<UbicacionMensaje> queryContactoSQL = await _ubicacionMensajeService.ObtenerTodos();

                List<UbicacionMensajeDTO> lista = queryContactoSQL
                    .Select(c => new UbicacionMensajeDTO()
                    {
                        Id = c.Id,
                        Id_Mensaje = c.Id_Mensaje,
                        Id_Asunto = c.Id_Asunto,
                        Id_Pais = c.Id_Pais,
                        Id_Seccion = c.Id_Seccion,
                        Id_SubSeccion = c.Id_SubSeccion,
                        Estado = c.Estado
                    }).ToList();

                return ResponseHelper.Success(lista, "Registro obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/UbicacionMensaje/5
        [HttpGet("GetUbicacionMensajeById")]
        public async Task<IActionResult> GetUbicacionMensajeById(int id)
        {
            try
            {
                var UbicacionMensaje = await _ubicacionMensajeService.ObtenerPorId(id);

                if (UbicacionMensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
                }

                var UbicacionMensajeDTO = new UbicacionMensajeDTO
                {
                    Id = UbicacionMensaje.Id,
                    Id_Mensaje = UbicacionMensaje.Id_Mensaje,
                    Id_Asunto = UbicacionMensaje.Id_Asunto,
                    Id_Pais = UbicacionMensaje.Id_Pais,
                    Id_Seccion = UbicacionMensaje.Id_Seccion,
                    Id_SubSeccion = UbicacionMensaje.Id_SubSeccion,
                    Estado = UbicacionMensaje.Estado
                };

                return ResponseHelper.Success(UbicacionMensajeDTO, "Registro obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/UbicacionMensaje/5
        [HttpPut("PutUbicacionMensaje")]
        public async Task<IActionResult> PutUbicacionMensaje(UbicacionMensajeDTO modelo)
        {
            try
            {
                var UbicacionMensajeExistente = await _ubicacionMensajeService.ObtenerPorId(modelo.Id);

                if (UbicacionMensajeExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
                }

                UbicacionMensajeExistente.Id_Mensaje = modelo.Id_Mensaje ?? UbicacionMensajeExistente.Id_Mensaje;
                UbicacionMensajeExistente.Id_Asunto = modelo.Id_Asunto ?? UbicacionMensajeExistente.Id_Asunto;
                UbicacionMensajeExistente.Id_Pais = modelo.Id_Pais ?? UbicacionMensajeExistente.Id_Pais;
                UbicacionMensajeExistente.Id_Seccion = modelo.Id_Seccion ?? UbicacionMensajeExistente.Id_Seccion;
                UbicacionMensajeExistente.Id_SubSeccion = modelo.Id_SubSeccion ?? UbicacionMensajeExistente.Id_SubSeccion;
                UbicacionMensajeExistente.Estado = modelo.Estado ?? UbicacionMensajeExistente.Estado;

                bool respuesta = await _ubicacionMensajeService.Actualizar(UbicacionMensajeExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar la ubicacion.");
                }

                return ResponseHelper.Success("Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/UbicacionMensaje
        [HttpPost("PostUbicacionMensaje")]
        public async Task<IActionResult> PostUbicacionMensaje(UbicacionMensajeDTO modelo)
        {
            try
            {
                UbicacionMensaje nuevoModelo = new UbicacionMensaje()
                {
                    Id_Mensaje = modelo.Id_Mensaje,
                    Id_Asunto = modelo.Id_Asunto,
                    Id_Pais = modelo.Id_Pais,
                    Id_Seccion = modelo.Id_Seccion,
                    Id_SubSeccion = modelo.Id_SubSeccion,
                    Estado = true
                };

                bool respuesta = await _ubicacionMensajeService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo insertar la ubicacion.");
                }

                return ResponseHelper.Success("Registro insertado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/UbicacionMensaje/5
        [HttpDelete("DeleteUbicacionMensaje")]
        public async Task<IActionResult> DeleteUbicacionMensaje(int id)
        {
            try
            {
                var UbicacionMensaje = await _ubicacionMensajeService.ObtenerPorId(id);
                if (UbicacionMensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
                }

                bool respuesta = await _ubicacionMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar la ubicacion.");
                }

                return ResponseHelper.Success("Registro eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
