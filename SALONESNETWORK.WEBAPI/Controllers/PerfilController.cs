using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.WEBAPI.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;
using SALONESNETWORK.BLL.Helpers;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET: api/Perfil
        [HttpGet("GetPerfiles")]
        public async Task<IActionResult> GetPerfiles()
        {
            try
            {
                IQueryable<Perfil> queryContactoSQL = await _perfilService.ObtenerTodos();
                List<PerfilDTO> lista = queryContactoSQL
                    .Select(c => new PerfilDTO()
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

                return ResponseHelper.Success(lista, "Perfiles obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/Perfil/5
        [HttpGet("GetPerfilById")]
        public async Task<IActionResult> GetPerfilById(int id)
        {
            try
            {
                var perfil = await _perfilService.ObtenerPorId(id);

                if (perfil == null)
                {
                    return ResponseHelper.NotFoundResponse("El perfil no fue encontrado.");
                }

                var perfilDTO = new PerfilDTO
                {
                    Id = perfil.Id,
                    Nombre = perfil.Nombre,
                    Descripcion = perfil.Descripcion,
                    FechaCreacion = perfil.FechaCreacion,
                    UsuarioCreacion = perfil.UsuarioCreacion,
                    FechaModificacion = perfil.FechaModificacion,
                    UsuarioModificacion = perfil.UsuarioModificacion,
                    Estado = perfil.Estado
                };

                return ResponseHelper.Success(perfilDTO, "Perfil obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/Perfil/5
        [HttpPut("PutPerfil")]
        public async Task<IActionResult> PutPerfil(PerfilDTO modelo)
        {
            try
            {
                var perfilExistente = await _perfilService.ObtenerPorId(modelo.Id);

                if (perfilExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("El perfil no fue encontrado.");
                }

                perfilExistente.Nombre = modelo.Nombre ?? perfilExistente.Nombre;
                perfilExistente.Descripcion = modelo.Descripcion ?? perfilExistente.Descripcion;
                perfilExistente.FechaModificacion = DateTime.Now;
                perfilExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? perfilExistente.UsuarioModificacion;
                perfilExistente.Estado = modelo.Estado ?? perfilExistente.Estado;

                bool respuesta = await _perfilService.Actualizar(perfilExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el perfil.");
                }

                return ResponseHelper.Success("Perfil actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/Perfil
        [HttpPost("PostPerfil")]
        public async Task<IActionResult> PostPerfil(PerfilDTO modelo)
        {
            try
            {
                Perfil nuevoModelo = new Perfil()
                {
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = 1,
                    Estado = true
                };

                bool respuesta = await _perfilService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el perfil.");
                }

                return ResponseHelper.Success("Perfil creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/Perfil/5
        [HttpDelete("DeletePerfil")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            try
            {
                var perfil = await _perfilService.ObtenerPorId(id);

                if (perfil == null)
                {
                    return ResponseHelper.NotFoundResponse("El perfil no fue encontrado.");
                }

                bool respuesta = await _perfilService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el perfil.");
                }

                return ResponseHelper.Success("Perfil eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
