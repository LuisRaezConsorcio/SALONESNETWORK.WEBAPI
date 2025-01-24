using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.WEBAPI.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.BLL.Services;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;
using SALONESNETWORK.BLL.Helpers;

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

                return ResponseHelper.Success(lista, "Lista de UsuarioSecciones obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("La sección de usuario no fue encontrada.");
                }

                var usuarioSeccionDTO = new UsuarioSeccionDTO
                {
                    Id = usuarioSeccion.Id,
                    Id_Usuario = usuarioSeccion.Id_Usuario,
                    Id_Seccion = usuarioSeccion.Id_Seccion,
                    Estado = usuarioSeccion.Estado
                };

                return ResponseHelper.Success(usuarioSeccionDTO, "UsuarioSeccion obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("La sección de usuario no fue encontrada.");
                }

                usuarioSeccionExistente.Id_Usuario = modelo.Id_Usuario ?? usuarioSeccionExistente.Id_Usuario;
                usuarioSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? usuarioSeccionExistente.Id_Seccion;
                usuarioSeccionExistente.Estado = modelo.Estado ?? usuarioSeccionExistente.Estado;

                bool respuesta = await _usuarioSeccionService.Actualizar(usuarioSeccionExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar la sección de usuario.");
                }

                return ResponseHelper.Success("Sección de usuario actualizada con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo crear la sección de usuario.");
                }

                return ResponseHelper.Success("Sección de usuario creada con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("La sección de usuario no fue encontrada.");
                }

                bool respuesta = await _usuarioSeccionService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar la sección de usuario.");
                }

                return ResponseHelper.Success("Sección de usuario eliminada con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

    }
}
