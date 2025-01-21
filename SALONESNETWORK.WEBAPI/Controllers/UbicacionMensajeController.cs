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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Consulta exitosa.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los datos.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El país no fue encontrado.", Resultado = false });
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Consulta exitosa.", Datos = UbicacionMensajeDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los datos.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "No se encontró el registro.", Resultado = false });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar la ubicacion.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Actualización exitosa.", Resultado = respuesta});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar los datos.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar la ubicacion.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Inserción exitosa.", Resultado = respuesta});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al insertar los datos.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El país no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _ubicacionMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar la ubicacion.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status204NoContent, new { Mensaje = "Eliminación exitosa.", Resultado = respuesta});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar los datos.", Error = ex.Message });
            }
        }
    }
}
