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

                return ResponseHelper.Success(lista, "Lista de asuntos obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El asunto no fue encontrado.");
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

                return ResponseHelper.Success(AsuntoDTO, "Asunto obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El asunto no fue encontrado.");
                }

                AsuntoExistente.Nombre = modelo.Nombre ?? AsuntoExistente.Nombre;
                AsuntoExistente.Descripcion = modelo.Descripcion ?? AsuntoExistente.Descripcion;
                AsuntoExistente.FechaModificacion = DateTime.Now;
                AsuntoExistente.UsuarioModificacion = 1;
                AsuntoExistente.Estado = true;

                bool respuesta = await _asuntoService.Actualizar(AsuntoExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el asunto.");
                }

                return ResponseHelper.Success("Asunto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el asunto.");
                }

                return ResponseHelper.Success("Asunto creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El asunto no fue encontrado.");
                }

                bool respuesta = await _asuntoService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el asunto.");
                }

                return ResponseHelper.Success("El asunto fue eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
