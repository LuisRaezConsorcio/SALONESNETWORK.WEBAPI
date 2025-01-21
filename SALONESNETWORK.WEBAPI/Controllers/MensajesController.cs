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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Mensajes obtenidos exitosamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los mensajes.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El mensaje no fue encontrado.", Resultado = false });
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Mensaje obtenido exitosamente.", Datos = mensajeDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener el mensaje por ID.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El mensaje no existe.", Resultado = false });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el mensaje.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El mensaje fue actualizado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar el mensaje.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el mensaje.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El mensaje fue creado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear el mensaje.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El mensaje no existe.", Resultado = false });
                }

                bool respuesta = await _mensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el mensaje.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El mensaje fue eliminado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar el mensaje.", Error= ex.Message });
            }
        }
    }
}
