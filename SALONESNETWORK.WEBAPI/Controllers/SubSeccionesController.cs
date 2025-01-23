using System;
using System.Collections;
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
    public class SubSeccionesController : ControllerBase
    {

        private readonly ISubSeccionService _subSeccioneService;

        public SubSeccionesController(ISubSeccionService subSeccioneService)
        {
            _subSeccioneService = subSeccioneService;
        }

        // GET: api/SubSeccione
        [HttpGet("GetSubSecciones")]
        public async Task<IActionResult> GetSubSecciones()
        {
            try
            {
                IQueryable<SubSeccion> queryContactoSQL = await _subSeccioneService.ObtenerTodos();

                List<SubSeccionDTO> lista = queryContactoSQL
                    .Select(c => new SubSeccionDTO
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

                return ResponseHelper.Success(lista, "SubSecciones obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/SubSeccione/5
        [HttpGet("GetSubSeccionById")]
        public async Task<IActionResult> GetSubSeccionById(int id)
        {
            try
            {
                var SubSeccione = await _subSeccioneService.ObtenerPorId(id);

                if (SubSeccione == null)
                {
                    return ResponseHelper.NotFoundResponse("La subsección no fue encontrado.");
                }

                var SubSeccioneDTO = new SubSeccionDTO
                {
                    Id = SubSeccione.Id,
                    Nombre = SubSeccione.Nombre,
                    Descripcion = SubSeccione.Descripcion,
                    FechaCreacion = SubSeccione.FechaCreacion,
                    UsuarioCreacion = SubSeccione.UsuarioCreacion,
                    FechaModificacion = SubSeccione.FechaModificacion,
                    UsuarioModificacion = SubSeccione.UsuarioModificacion,
                    Estado = SubSeccione.Estado
                };

                return ResponseHelper.Success(SubSeccioneDTO, "SubSeccion obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/SubSeccione/5
        [HttpPut("PutSubSeccion")]
        public async Task<IActionResult> PutSubSeccion(SubSeccionDTO modelo)
        {
            try
            {
                var SubSeccioneExistente = await _subSeccioneService.ObtenerPorId(modelo.Id);

                if (SubSeccioneExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("La subsección no fue encontrada.");
                }

                SubSeccioneExistente.Nombre = modelo.Nombre ?? SubSeccioneExistente.Nombre;
                SubSeccioneExistente.Descripcion = modelo.Descripcion ?? SubSeccioneExistente.Descripcion;
                SubSeccioneExistente.FechaModificacion = DateTime.Now;
                SubSeccioneExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? SubSeccioneExistente.UsuarioModificacion;
                SubSeccioneExistente.Estado = modelo.Estado ?? SubSeccioneExistente.Estado;

                bool respuesta = await _subSeccioneService.Actualizar(SubSeccioneExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar la subsección.");
                }

                return ResponseHelper.Success("Subsección actualizada con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/SubSeccione
        [HttpPost("PostSubSeccion")]
        public async Task<IActionResult> PostSubSeccion(SubSeccionDTO modelo)
        {
            try
            {
                SubSeccion NuevoModelo = new SubSeccion
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = 1,
                    Estado = true
                };

                bool respuesta = await _subSeccioneService.Insertar(NuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo registrar la subsección.");
                }

                return ResponseHelper.Success("Subsección creada correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/SubSeccione/5
        [HttpDelete("DeleteSubSeccion")]
        public async Task<IActionResult> DeleteSubSeccion(int id)
        {
            try
            {
                var SubSeccione = await _subSeccioneService.ObtenerPorId(id);

                if (SubSeccione == null)
                {
                    return ResponseHelper.NotFoundResponse("La subsección no fue encontrado.");
                }

                bool respuesta = await _subSeccioneService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar la subsección.");
                }

                return ResponseHelper.Success("Subsección eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
