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
    public class SeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly ISeccionService _seccioneService;

        public SeccionesController(ISeccionService seccioneService)
        {
            _seccioneService = seccioneService;
        }

        // GET: api/Seccione
        [HttpGet("GetSecciones")]
        public async Task<IActionResult> GetSecciones()
        {
            try
            {
                IQueryable<Seccion> querySecciones = await _seccioneService.ObtenerTodos();

                List<SeccionDTO> lista = querySecciones
                    .Select(c => new SeccionDTO
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Secciones obtenidas correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener las secciones.", Error = ex.Message });
            }
        }

        // GET: api/Seccione/5
        [HttpGet("GetSeccionById")]
        public async Task<IActionResult> GetSeccionById(int id)
        {
            try
            {
                var seccion = await _seccioneService.ObtenerPorId(id);

                if (seccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección no fue encontrada.", Resultado = false });
                }

                var seccionDTO = new SeccionDTO
                {
                    Id = seccion.Id,
                    Nombre = seccion.Nombre,
                    Descripcion = seccion.Descripcion,
                    FechaCreacion = seccion.FechaCreacion,
                    UsuarioCreacion = seccion.UsuarioCreacion,
                    FechaModificacion = seccion.FechaModificacion,
                    UsuarioModificacion = seccion.UsuarioModificacion,
                    Estado = seccion.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección obtenida correctamente.", Datos = seccionDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener la sección.", Error = ex.Message });
            }
        }

        // PUT: api/Seccione/5
        [HttpPut("PutSeccion")]
        public async Task<IActionResult> PutSeccion(SeccionDTO modelo)
        {
            try
            {
                var seccionExistente = await _seccioneService.ObtenerPorId(modelo.Id);

                if (seccionExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección no existe.", Resultado = false });
                }

                seccionExistente.Nombre = modelo.Nombre ?? seccionExistente.Nombre;
                seccionExistente.Descripcion = modelo.Descripcion ?? seccionExistente.Descripcion;
                seccionExistente.FechaModificacion = DateTime.Now;
                seccionExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? seccionExistente.UsuarioModificacion;
                seccionExistente.Estado = modelo.Estado ?? seccionExistente.Estado;

                bool respuesta = await _seccioneService.Actualizar(seccionExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar la sección.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección actualizada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar la sección.", Error = ex.Message });
            }
        }

        // POST: api/Seccione
        [HttpPost("PostSeccion")]
        public async Task<IActionResult> PostSeccion(SeccionDTO modelo)
        {
            try
            {
                Seccion nuevoModelo = new Seccion
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = modelo.UsuarioCreacion ?? 1,
                    Estado = true
                };

                bool respuesta = await _seccioneService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo registrar la sección.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Sección creada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al registrar la sección.", Error = ex.Message });
            }
        }

        // DELETE: api/Seccione/5
        [HttpDelete("DeleteSeccion")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            try
            {
                var seccion = await _seccioneService.ObtenerPorId(id);

                if (seccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La sección no fue encontrada.", Resultado = false });
                }

                bool respuesta = await _seccioneService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar la sección.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Sección eliminada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar la sección.", Error = ex.Message });
            }
        }
    }
}
