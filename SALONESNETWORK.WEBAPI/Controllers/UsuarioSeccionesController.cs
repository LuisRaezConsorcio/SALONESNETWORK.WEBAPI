using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.BLL.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.BLL.Services;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioSeccionesController : ControllerBase
    {

        private readonly IUsuarioSeccionService _usuarioSeccionService;

        public UsuarioSeccionesController(IUsuarioSeccionService usuarioSeccionService)
        {
            _usuarioSeccionService = usuarioSeccionService;
        }

        // GET: api/UsuarioSeccion
        [HttpGet("GetUsuarioSecciones")]
        public async Task<IActionResult> GetUsuarioSecciones()
        {
            try
            {
                IQueryable<UsuarioSeccion> query = await _usuarioSeccionService.ObtenerTodos();

                List<UsuarioSeccionDTO> lista = query.Select(c => new UsuarioSeccionDTO
                {
                    Id = c.Id,
                    Id_Usuario = c.Id_Usuario,
                    Id_Seccion = c.Id_Seccion,
                    Estado = c.Estado
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Lista de UsuarioSecciones obtenida correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener las secciones de usuario.", Error = ex.Message });
            }
        }

        // GET: api/UsuarioSeccion/5
        [HttpGet("GetUsuarioSeccionById")]
        public async Task<IActionResult> GetUsuarioSeccionById(int id)
        {
            try
            {
                var usuarioSeccion = await _usuarioSeccionService.ObtenerPorId(id);

                if (usuarioSeccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección de usuario no fue encontrada.", Resultado = false });
                }

                var usuarioSeccionDTO = new UsuarioSeccionDTO
                {
                    Id = usuarioSeccion.Id,
                    Id_Usuario = usuarioSeccion.Id_Usuario,
                    Id_Seccion = usuarioSeccion.Id_Seccion,
                    Estado = usuarioSeccion.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = " UsuarioSeccion obtenido correctamente.", Datos = usuarioSeccionDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener la sección de usuario.", Error = ex.Message });
            }
        }

        // PUT: api/UsuarioSeccion/5
        [HttpPut("PutUsuarioSeccion")]
        public async Task<IActionResult> PutUsuarioSeccion(UsuarioSeccionDTO modelo)
        {
            try
            {
                var usuarioSeccionExistente = await _usuarioSeccionService.ObtenerPorId(modelo.Id);

                if (usuarioSeccionExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección de usuario no fue encontrada.", Resultado = false});
                }

                usuarioSeccionExistente.Id_Usuario = modelo.Id_Usuario ?? usuarioSeccionExistente.Id_Usuario;
                usuarioSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? usuarioSeccionExistente.Id_Seccion;
                usuarioSeccionExistente.Estado = modelo.Estado ?? usuarioSeccionExistente.Estado;

                bool respuesta = await _usuarioSeccionService.Actualizar(usuarioSeccionExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar la sección de usuario.", Resultado = false});
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección de usuario actualizada con éxito.", Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar la sección de usuario.", Error = ex.Message });
            }
        }

        // POST: api/UsuarioSeccion
        [HttpPost("PostUsuarioSeccion")]
        public async Task<IActionResult> PostUsuarioSeccion(UsuarioSeccionDTO modelo)
        {
            try
            {
                UsuarioSeccion nuevoModelo = new UsuarioSeccion
                {
                    Id_Usuario = modelo.Id_Usuario,
                    Id_Seccion = modelo.Id_Seccion,
                    Estado = true
                };

                bool respuesta = await _usuarioSeccionService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo crear la sección de usuario.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección de usuario creada con éxito.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al crear la sección de usuario.", Error = ex.Message });
            }
        }

        // DELETE: api/UsuarioSeccion/5
        [HttpDelete("DeleteUsuarioSeccion")]
        public async Task<IActionResult> DeleteUsuarioSeccion(int id)
        {
            try
            {
                var usuarioSeccion = await _usuarioSeccionService.ObtenerPorId(id);

                if (usuarioSeccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección de usuario no fue encontrada.", Resultado = false});
                }

                bool respuesta = await _usuarioSeccionService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar la sección de usuario.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección de usuario eliminada con éxito.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar la sección de usuario.", Error= ex.Message });
            }
        }

    }
}
