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
using static System.Runtime.InteropServices.JavaScript.JSType;
using SALONESNETWORK.BLL.Helpers;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {

        private readonly IMensajeService _mensajeService;

        public MensajesController(IMensajeService mensajeService)
        {
            _mensajeService = mensajeService;
        }

        // GET: api/Mensaje
        [HttpGet("GetMensajes")]
        public async Task<IActionResult> GetMensajes()
        {
            try
            {
                var queryContactoSQL = await _mensajeService.ObtenerTodos();

                var lista = queryContactoSQL
                    .Select(c => new MensajeDTO
                    {
                        Id = c.Id,
                        Id_TipoMensaje = c.Id_TipoMensaje,
                        Id_Usuario = c.Id_Usuario,
                        Nombre = c.Nombre,
                        Descripcion = c.Descripcion,
                        FechaCreacion = c.FechaCreacion,
                        UsuarioCreacion = c.UsuarioCreacion,
                        FechaModificacion = c.FechaModificacion,
                        UsuarioModificacion = c.UsuarioModificacion,
                        Seguimiento = c.Seguimiento,
                        Id_MensajeSeguimiento = c.Id_MensajeSeguimiento,
                        Respuesta = c.Respuesta,
                        Id_MensajeRespuesta = c.Id_MensajeRespuesta,
                        Estado = c.Estado
                    })
                    .ToList();

                return ResponseHelper.Success(lista, "Mensajes obtenidos exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/Mensaje/5
        [HttpGet("GetMensajeById")]
        public async Task<IActionResult> GetMensajeById(int id)
        {
            try
            {
                var mensaje = await _mensajeService.ObtenerPorId(id);

                if (mensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El mensaje no fue encontrado.");
                }

                var mensajeDTO = new MensajeDTO
                {
                    Id = mensaje.Id,
                    Id_TipoMensaje = mensaje.Id_TipoMensaje,
                    Id_Usuario = mensaje.Id_Usuario,
                    Nombre = mensaje.Nombre,
                    Descripcion = mensaje.Descripcion,
                    FechaCreacion = mensaje.FechaCreacion,
                    UsuarioCreacion = mensaje.UsuarioCreacion,
                    FechaModificacion = mensaje.FechaModificacion,
                    UsuarioModificacion = mensaje.UsuarioModificacion,
                    Seguimiento = mensaje.Seguimiento,
                    Id_MensajeSeguimiento = mensaje.Id_MensajeSeguimiento,
                    Respuesta = mensaje.Respuesta,
                    Id_MensajeRespuesta = mensaje.Id_MensajeRespuesta,
                    Estado = mensaje.Estado
                };

                return ResponseHelper.Success(mensajeDTO, "Mensaje obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/Mensaje/5
        [HttpPut("PutMensaje")]
        public async Task<IActionResult> PutMensaje(MensajeDTO modelo)
        {
            try
            {
                var mensajeExistente = await _mensajeService.ObtenerPorId(modelo.Id);

                if (mensajeExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("El mensaje no fue encontrado.");
                }

                mensajeExistente.Id_TipoMensaje = modelo.Id_TipoMensaje ?? mensajeExistente.Id_TipoMensaje;
                mensajeExistente.Id_Usuario = modelo.Id_Usuario ?? mensajeExistente.Id_Usuario;
                mensajeExistente.Nombre = modelo.Nombre ?? mensajeExistente.Nombre;
                mensajeExistente.Descripcion = modelo.Descripcion ?? mensajeExistente.Descripcion;
                mensajeExistente.FechaModificacion = DateTime.Now;
                mensajeExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? mensajeExistente.UsuarioModificacion;
                mensajeExistente.Seguimiento = modelo.Seguimiento ?? mensajeExistente.Seguimiento;
                mensajeExistente.Id_MensajeSeguimiento = modelo.Id_MensajeSeguimiento ?? mensajeExistente.Id_MensajeSeguimiento;
                mensajeExistente.Respuesta = modelo.Respuesta ?? mensajeExistente.Respuesta;
                mensajeExistente.Id_MensajeRespuesta = modelo.Id_MensajeRespuesta ?? mensajeExistente.Id_MensajeRespuesta;
                mensajeExistente.Estado = modelo.Estado ?? mensajeExistente.Estado;

                var respuesta = await _mensajeService.Actualizar(mensajeExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el mensaje.");
                }

                return ResponseHelper.Success("El mensaje fue actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/Mensaje
        [HttpPost("PostMensaje")]
        public async Task<IActionResult> PostMensaje(MensajeDTO modelo)
        {
            try
            {
                var nuevoModelo = new Mensaje
                {
                    Id_TipoMensaje = modelo.Id_TipoMensaje,
                    Id_Usuario = modelo.Id_Usuario,
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = modelo.UsuarioCreacion ?? 1,
                    Seguimiento = modelo.Seguimiento,
                    Id_MensajeSeguimiento = modelo.Id_MensajeSeguimiento,
                    Respuesta = modelo.Respuesta,
                    Id_MensajeRespuesta = modelo.Id_MensajeRespuesta,
                    Estado = true
                };

                var respuesta = await _mensajeService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el mensaje.");
                }

                return ResponseHelper.Success("El mensaje fue creado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/Mensaje/5
        [HttpDelete("DeleteMensaje")]
        public async Task<IActionResult> DeleteMensaje(int id)
        {
            try
            {
                var mensaje = await _mensajeService.ObtenerPorId(id);

                if (mensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El mensaje no fue encontrado.");
                }

                bool respuesta = await _mensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el mensaje.");
                }

                return ResponseHelper.Success("El mensaje fue eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
