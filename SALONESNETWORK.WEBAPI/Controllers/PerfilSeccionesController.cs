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
    public class PerfilSeccionesController : ControllerBase
    {

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

                return ResponseHelper.Success(lista, "PerfilSecciones obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El PerfilSeccion no fue encontrado.");
                }

                var perfilSeccionDTO = new PerfilSeccionDTO
                {
                    Id = perfilSeccion.Id,
                    Id_Seccion = perfilSeccion.Id_Seccion,
                    Id_Perfil = perfilSeccion.Id_Perfil,
                    Estado = perfilSeccion.Estado
                };

                return ResponseHelper.Success(perfilSeccionDTO, "PerfilSeccion obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El PerfilSeccion no fue encontrado.");
                }

                perfilSeccionExistente.Id_Seccion = modelo.Id_Seccion ?? perfilSeccionExistente.Id_Seccion;
                perfilSeccionExistente.Id_Perfil = modelo.Id_Perfil ?? perfilSeccionExistente.Id_Perfil;
                perfilSeccionExistente.Estado = modelo.Estado ?? perfilSeccionExistente.Estado;

                bool respuesta = await _perfilSeccionService.Actualizar(perfilSeccionExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el registro.");
                }

                return ResponseHelper.Success("PerfilSeccion actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el registro.");
                }

                return ResponseHelper.Success("PerfilSeccion creada correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El PerfilSeccion no fue encontrado.");
                }

                bool respuesta = await _perfilSeccionService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el regsitro.");
                }

                return ResponseHelper.Success("PerfilSeccion eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
