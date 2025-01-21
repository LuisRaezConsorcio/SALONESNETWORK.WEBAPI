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
    public class PerfilSeccionesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IPerfilSeccionService _perfilSeccionService;

        public PerfilSeccionesController(IPerfilSeccionService perfilSeccionService)
        {
            _perfilSeccionService = perfilSeccionService;
        }

        // GET: api/PerfilSeccion
        [HttpGet("GetPerfilSecciones")]
        public async Task<IActionResult> GetPerfilSecciones()
        {
            try
            {
                IQueryable<PerfilSeccion> queryContactoSQL = await _perfilSeccionService.ObtenerTodos();
                List<PerfilSeccionDTO> lista = queryContactoSQL
                    .Select(c => new PerfilSeccionDTO()
                    {
                        Id = c.Id,
                        Id_Seccion = c.Id_Seccion,
                        Id_Perfil = c.Id_Perfil,
                        Estado = c.Estado
                    }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "PerfilSecciones obtenidas correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener las PerfilSecciones.", Error = ex.Message });
            }
        }

        // GET: api/PerfilSeccion/5
        [HttpGet("GetPerfilSeccionById")]
        public async Task<IActionResult> GetPerfilSeccionById(int id)
        {
            try
            {
                var perfilSeccion = await _perfilSeccionService.ObtenerPorId(id);

                if (perfilSeccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La PerfilSeccion no fue encontrada.", Resultado = false });
                }

                var perfilSeccionDTO = new PerfilSeccionDTO
                {
                    Id = perfilSeccion.Id,
                    Id_Seccion = perfilSeccion.Id_Seccion,
                    Id_Perfil = perfilSeccion.Id_Perfil,
                    Estado = perfilSeccion.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "PerfilSeccion obtenida correctamente.", Datos = perfilSeccionDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener la PerfilSeccion.", Error = ex.Message });
            }
        }

        // PUT: api/PerfilSeccion/5
        [HttpPut("PutPerfilSeccion")]
        public async Task<IActionResult> PutPerfilSeccion(PerfilSeccionDTO modelo)
        {
            try
            {
                var perfilSeccionExistente = await _perfilSeccionService.ObtenerPorId(modelo.Id);

                if (perfilSeccionExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La PerfilSeccion no existe.", Resultado = false });
                }

                perfilSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? perfilSeccionExistente.Id_Seccion;
                perfilSeccionExistente.Id_Perfil = modelo.Id_Perfil ?? perfilSeccionExistente.Id_Perfil;
                perfilSeccionExistente.Estado = modelo.Estado ?? perfilSeccionExistente.Estado;

                bool respuesta = await _perfilSeccionService.Actualizar(perfilSeccionExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "PerfilSeccion actualizada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar la PerfilSeccion.", Error = ex.Message });
            }
        }

        // POST: api/PerfilSeccion
        [HttpPost("PostPerfilSeccion")]
        public async Task<IActionResult> PostPerfilSeccion(PerfilSeccionDTO modelo)
        {
            try
            {
                PerfilSeccion nuevoModelo = new PerfilSeccion()
                {
                    Id_Seccion = modelo.Id_Seccion,
                    Id_Perfil = modelo.Id_Perfil,
                    Estado = true
                };

                bool respuesta = await _perfilSeccionService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "PerfilSeccion creada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear la PerfilSeccion.", Error = ex.Message });
            }
        }

        // DELETE: api/PerfilSeccion/5
        [HttpDelete("DeletePerfilSeccion")]
        public async Task<IActionResult> DeletePerfilSeccion(int id)
        {
            try
            {
                var perfilSeccion = await _perfilSeccionService.ObtenerPorId(id);

                if (perfilSeccion == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "La PerfilSeccion no fue encontrada.", Resultado = false });
                }

                bool respuesta = await _perfilSeccionService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el regsitro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "PerfilSeccion eliminada correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar la PerfilSeccion.", Error = ex.Message });
            }
        }
    }
}
