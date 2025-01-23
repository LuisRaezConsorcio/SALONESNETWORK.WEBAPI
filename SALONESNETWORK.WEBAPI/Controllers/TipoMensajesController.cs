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
    public class TipoMensajesController : ControllerBase
    {

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

                return ResponseHelper.Success(lista, "Los tipomensajes fueron obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El tipo de mensaje no fue encontrado.");
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

                return ResponseHelper.Success(tipoMensajeDTO, "El tipoMensaje fue encontrado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El tipo de mensaje no fue encontrado.");
                }

                tipoMensajeExistente.Nombre = modelo.Nombre ?? tipoMensajeExistente.Nombre;
                tipoMensajeExistente.Descripcion = modelo.Descripcion ?? tipoMensajeExistente.Descripcion;
                tipoMensajeExistente.FechaModificacion = DateTime.Now;
                tipoMensajeExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? tipoMensajeExistente.UsuarioModificacion;
                tipoMensajeExistente.Estado = modelo.Estado ?? tipoMensajeExistente.Estado;

                bool respuesta = await _tipoMensajeService.Actualizar(tipoMensajeExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el registro.");
                }

                return ResponseHelper.Success("Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el registro.");
                }

                return ResponseHelper.Success("Registro insertado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El tipo de mensaje no fue encontrado.");
                }

                bool respuesta = await _tipoMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("\"No se pudo eliminar el registro.");
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
