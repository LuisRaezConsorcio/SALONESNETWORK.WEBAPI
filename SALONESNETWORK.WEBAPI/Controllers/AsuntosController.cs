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
    public class AsuntoController : ControllerBase
    {

        private readonly IAsuntoService _asuntoService;

        public AsuntoController(IAsuntoService asuntoService)
        {
            _asuntoService = asuntoService;
        }

        // GET: api/Asunto
        [HttpGet("GetAsuntos")]
        public async Task<IActionResult> GetAsuntos()
        {
            try
            {
                IQueryable<Asunto> queryContactoSQL = await _asuntoService.ObtenerTodos();

                List<AsuntoDTO> lista = queryContactoSQL
                                         .Select(c => new AsuntoDTO()
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Lista de asuntos obtenida correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener los asuntos.", Error = ex.Message });
            }
        }

        // GET: api/Asunto/5
        [HttpGet("GetAsuntoById")]
        public async Task<IActionResult> GetAsuntoById(int id)
        {
            try
            {
                var Asunto = await _asuntoService.ObtenerPorId(id);

                if (Asunto == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El asunto no fue encontrado.", Resultado = false });
                }

                var AsuntoDTO = new AsuntoDTO
                {
                    Id = Asunto.Id,
                    Nombre = Asunto.Nombre,
                    Descripcion = Asunto.Descripcion,
                    FechaCreacion = Asunto.FechaCreacion,
                    UsuarioCreacion = Asunto.UsuarioCreacion,
                    FechaModificacion = Asunto.FechaModificacion,
                    UsuarioModificacion = Asunto.UsuarioModificacion,
                    Estado = Asunto.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Asunto obtenido correctamente.", Datos = AsuntoDTO, Resultado=true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el asunto por ID.", Error = ex.Message });
            }
        }

        // PUT: api/Asunto/5
        [HttpPut("PutAsunto")]
        public async Task<IActionResult> PutAsunto(AsuntoDTO modelo)
        {
            try
            {
                var AsuntoExistente = await _asuntoService.ObtenerPorId(modelo.Id);

                if (AsuntoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El asunto no existe.", Resultado = false });
                }

                AsuntoExistente.Nombre = modelo.Nombre ?? AsuntoExistente.Nombre;
                AsuntoExistente.Descripcion = modelo.Descripcion ?? AsuntoExistente.Descripcion;
                AsuntoExistente.FechaModificacion = DateTime.Now;
                AsuntoExistente.UsuarioModificacion = 1;
                AsuntoExistente.Estado = modelo.Estado ?? AsuntoExistente.Estado;

                bool respuesta = await _asuntoService.Actualizar(AsuntoExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el asunto.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Asunto actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar el asunto.", Error = ex.Message });
            }
        }

        // POST: api/Asunto
        [HttpPost("PostAsunto")]
        public async Task<IActionResult> PostAsunto(AsuntoDTO modelo)
        {

            try
            {
                Asunto NuevoModelo = new Asunto()
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = modelo.UsuarioCreacion,
                    Estado = true
                };

                bool respuesta = await _asuntoService.Insertar(NuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el asunto.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Asunto creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al crear el asunto.", Error = ex.Message });
            }

        }

        // DELETE: api/Asunto/5
        [HttpDelete("DeleteAsunto")]
        public async Task<IActionResult> DeleteAsunto(int id)
        {
            try
            {
                var Asunto = await _asuntoService.ObtenerPorId(id);

                if (Asunto == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El asunto no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _asuntoService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el asunto.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El asunto fue eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el asunto.", Error = ex.Message });
            }
        }
    }
}
