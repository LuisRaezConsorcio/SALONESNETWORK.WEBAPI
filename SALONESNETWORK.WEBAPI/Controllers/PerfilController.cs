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
    public class PerfilController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfiles obtenidos correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los perfiles.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El perfil no fue encontrado.", Resultado = false });
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfil obtenido correctamente.", Datos = perfilDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener el perfil.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El perfil no existe.", Resultado = false });
                }

                perfilExistente.Nombre = modelo.Nombre ?? perfilExistente.Nombre;
                perfilExistente.Descripcion = modelo.Descripcion ?? perfilExistente.Descripcion;
                perfilExistente.FechaModificacion = DateTime.Now;
                perfilExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? perfilExistente.UsuarioModificacion;
                perfilExistente.Estado = modelo.Estado ?? perfilExistente.Estado;

                bool respuesta = await _perfilService.Actualizar(perfilExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el perfil.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfil actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar el perfil.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el perfil.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Perfil creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear el perfil.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El perfil no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _perfilService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el perfil.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfil eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar el perfil.", Error = ex.Message });
            }
        }
    }
}
