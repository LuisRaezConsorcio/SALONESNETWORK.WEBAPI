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
    public class SeccionesController : ControllerBase
    {

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

                return ResponseHelper.Success(lista, "Registro obtenido correctamente.");// { mensaje = "Secciones obtenidas correctamente.", datos = lista, resultado = true });
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);//, new { mensaje = "Ocurrió un error al obtener las secciones.", error = ex.Message });
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");//{ mensaje = "La sección no fue encontrada.", resultado = false });
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

                return ResponseHelper.Success(seccionDTO, "Registro obtenido correctamente.");// { mensaje = "Sección obtenida correctamente.", datos = seccionDTO, resultado = true });
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);//, new { mensaje = "Ocurrió un error al obtener la sección.", error = ex.Message });
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");//{ mensaje = "La sección no existe.", resultado = false });
                }

                seccionExistente.Nombre = modelo.Nombre ?? seccionExistente.Nombre;
                seccionExistente.Descripcion = modelo.Descripcion ?? seccionExistente.Descripcion;
                seccionExistente.FechaModificacion = DateTime.Now;
                seccionExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? seccionExistente.UsuarioModificacion;
                seccionExistente.Estado = modelo.Estado ?? seccionExistente.Estado;

                bool respuesta = await _seccioneService.Actualizar(seccionExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("Ocurrió un error al generar la accion.");// { mensaje = "No se pudo actualizar la sección.", resultado = false });
                }

                return ResponseHelper.Success("Registro obtenido correctamente.");// { mensaje = "Sección actualizada correctamente.", resultado = respuesta });
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);//, new { mensaje = "Ocurrió un error al actualizar la sección.", error = ex.Message });
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
                    return ResponseHelper.BadRequestResponse("Ocurrió un error al generar la accion.");// { mensaje = "No se pudo registrar la sección.", resultado = false });
                }

                return ResponseHelper.Success("Registro obtenido correctamente.");// { mensaje = "Sección creada correctamente.", resultado = respuesta });
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);//, new { mensaje = "Ocurrió un error al registrar la sección.", error = ex.Message });
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");//{ mensaje = "La sección no fue encontrada.", resultado = false });
                }

                bool respuesta = await _seccioneService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("Ocurrió un error al generar la accion.");// { mensaje = "No se pudo eliminar la sección.", resultado = false });
                }

                return ResponseHelper.Success("Registro obtenido correctamente.");// { mensaje = "Sección eliminada correctamente.", resultado = respuesta });
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);//, new { mensaje = "Ocurrió un error al eliminar la sección.", error = ex.Message });
            }
        }
    }
}
