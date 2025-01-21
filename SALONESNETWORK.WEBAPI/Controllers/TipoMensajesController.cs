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
    public class TipoMensajesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly ITipoMensajeService _tipoMensajeService;

        public TipoMensajesController(ITipoMensajeService tipoMensajeService)
        {
            _tipoMensajeService = tipoMensajeService;
        }

        // GET: api/TipoMensaje
        [HttpGet("GetTipoMensajes")]
        public async Task<IActionResult> GetTipoMensajes()
        {
            try
            {
                IQueryable<TipoMensaje> queryTipoMensajeSQL = await _tipoMensajeService.ObtenerTodos();

                List<TipoMensajeDTO> lista = queryTipoMensajeSQL
                    .Select(c => new TipoMensajeDTO()
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Los tipomensajes fueron obtenidos correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener los tipos de mensaje", Error = ex.Message });
            }
        }

        // GET: api/TipoMensaje/5
        [HttpGet("GetTipoMensajeById")]
        public async Task<IActionResult> GetTipoMensajeById(int id)
        {
            try
            {
                var tipoMensaje = await _tipoMensajeService.ObtenerPorId(id);

                if (tipoMensaje == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El tipo de mensaje no fue encontrado.", Resultado = false });
                }

                var tipoMensajeDTO = new TipoMensajeDTO
                {
                    Id = tipoMensaje.Id,
                    Nombre = tipoMensaje.Nombre,
                    Descripcion = tipoMensaje.Descripcion,
                    FechaCreacion = tipoMensaje.FechaCreacion,
                    UsuarioCreacion = tipoMensaje.UsuarioCreacion,
                    FechaModificacion = tipoMensaje.FechaModificacion,
                    UsuarioModificacion = tipoMensaje.UsuarioModificacion,
                    Estado = tipoMensaje.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El tipoMensaje fue encontrado correctamente", Datos = tipoMensajeDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el tipo de mensaje", Error = ex.Message });
            }
        }

        // PUT: api/TipoMensaje/5
        [HttpPut("PutTipoMensaje")]
        public async Task<IActionResult> PutTipoMensaje(TipoMensajeDTO modelo)
        {
            try
            {
                var tipoMensajeExistente = await _tipoMensajeService.ObtenerPorId(modelo.Id);

                if (tipoMensajeExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El tipo de mensaje no existe.", Resultado = false });
                }

                tipoMensajeExistente.Nombre = modelo.Nombre ?? tipoMensajeExistente.Nombre;
                tipoMensajeExistente.Descripcion = modelo.Descripcion ?? tipoMensajeExistente.Descripcion;
                tipoMensajeExistente.FechaModificacion = DateTime.Now;
                tipoMensajeExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? tipoMensajeExistente.UsuarioModificacion;
                tipoMensajeExistente.Estado = modelo.Estado ?? tipoMensajeExistente.Estado;

                bool respuesta = await _tipoMensajeService.Actualizar(tipoMensajeExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Actualización exitosa.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {Mensaje = $"Ocurrió un error al actualizar el tipo de mensaje", Error = ex.Message });
            }
        }

        // POST: api/TipoMensaje
        [HttpPost("PostTipoMensaje")]
        public async Task<IActionResult> PostTipoMensaje(TipoMensajeDTO modelo)
        {
            try
            {
                var nuevoModelo = new TipoMensaje()
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = modelo.UsuarioCreacion ?? 1,
                    Estado = true
                };

                bool respuesta = await _tipoMensajeService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Registro exitoso.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al registrar el tipo de mensaje", Error = ex.Message });
            }
        }

        // DELETE: api/TipoMensaje/5
        [HttpDelete("DeleteTipoMensaje")]
        public async Task<IActionResult> DeleteTipoMensaje(int id)
        {
            try
            {
                var tipoMensaje = await _tipoMensajeService.ObtenerPorId(id);

                if (tipoMensaje == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El tipo de mensaje no existe.", Resultado = false });
                }

                bool respuesta = await _tipoMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status204NoContent, new { Mensaje = "Eliminación exitosa.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el tipo de mensaje", Error = ex.Message });
            }
        }
    }
}
