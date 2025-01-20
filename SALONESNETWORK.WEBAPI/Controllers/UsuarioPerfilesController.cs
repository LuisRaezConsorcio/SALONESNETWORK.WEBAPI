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
    public class UsuarioPerfilesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IUsuarioPerfilService _usuarioPerfilService;

        public UsuarioPerfilesController(IUsuarioPerfilService usuarioPerfilService)
        {
            _usuarioPerfilService = usuarioPerfilService;
        }

        // GET: api/UsuarioPerfil
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IQueryable<UsuarioPerfil> queryContactoSQL = await _usuarioPerfilService.ObtenerTodos();

                List<UsuarioPerfilDTO> lista = queryContactoSQL.Select(c => new UsuarioPerfilDTO()
                {
                    Id = c.Id,
                    Id_Usuario = c.Id_Usuario,
                    Id_Perfil = c.Id_Perfil
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { valor = lista, mensaje = "Lista obtenida correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener la lista.", detalle = ex.Message });
            }
        }

        // GET: api/GetUsuarioPerfilById/5
        [HttpGet("GetUsuarioPerfilIdByUsuarioPerfil")]
        public async Task<ActionResult> GetUsuarioPerfilIdByUsuarioPerfil(UsuarioPerfilDTO modelo)
        {
            try
            {
                UsuarioPerfil NuevoModelo = new UsuarioPerfil
                {
                    Id_Usuario = modelo.Id_Usuario,
                    Id_Perfil = modelo.Id_Perfil
                };

                int? UsuarioPerfilId = await _usuarioPerfilService.ObtenerId(NuevoModelo);

                if (UsuarioPerfilId == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { valor = UsuarioPerfilId, mensaje = "No hay un Usuario con el Perfil consultado." });
                }

                return StatusCode(StatusCodes.Status200OK, new { valor = UsuarioPerfilId, mensaje = "Id encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener el ID.", detalle = ex.Message });
            }
        }
        
        // GET: api/UsuarioPerfil/5
        [HttpGet("GetUsuarioPerfilByUsuario")]
        public async Task<ActionResult<IEnumerable<UsuarioPerfil>>> GetUsuarioPerfilByUsuario(int idusuario)
        {

            try
            {
                UsuarioPerfil NuevoModelo = new UsuarioPerfil
                {
                    Id_Usuario = idusuario
                };

                IQueryable<UsuarioPerfil> queryContactoSQL = await _usuarioPerfilService.ObtenerPorUsuario(NuevoModelo);

                List<UsuarioPerfilDTO> lista = queryContactoSQL.Select(c => new UsuarioPerfilDTO()
                {
                    Id = c.Id,
                    Id_Usuario = c.Id_Usuario,
                    Id_Perfil = c.Id_Perfil
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { valor = lista, mensaje = "Perfiles obtenidos correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener los perfiles.", detalle = ex.Message });
            }
        }

        // GET: api/GetUsuarioPerfilByPerfil/5
        [HttpGet("GetUsuarioPerfilByPerfil")]
        public async Task<ActionResult<IEnumerable<UsuarioPerfil>>> GetUsuarioPerfilByPerfil(int idperfil)
        {

            try
            {
                UsuarioPerfil NuevoModelo = new UsuarioPerfil
                {
                    Id_Perfil = idperfil
                };

                IQueryable<UsuarioPerfil> queryContactoSQL = await _usuarioPerfilService.ObtenerPorPerfil(NuevoModelo);

                List<UsuarioPerfilDTO> lista = queryContactoSQL.Select(c => new UsuarioPerfilDTO()
                {
                    Id = c.Id,
                    Id_Usuario = c.Id_Usuario,
                    Id_Perfil = c.Id_Perfil
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { valor = lista, mensaje = "Perfiles obtenidos correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener los perfiles.", detalle = ex.Message });
            }
        }

        //PUT: api/UsuarioPerfil/5
        //  To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("PutUsuarioPerfil")]
        //public async Task<IActionResult> PutUsuarioPerfil(UsuarioPerfilDTO modelo)
        //{

        //    UsuarioPerfil NuevoModelo = new UsuarioPerfil()
        //    {
        //        Id_Usuario = modelo.Id_Usuario,
        //        Id_Perfil = modelo.Id_Perfil
        //    };

        //    //Buscar el modelo existente en la base de datos
        //    var UsuarioPerfilExistente = await _usuarioPerfilService.ObtenerPorId(NuevoModelo);

        //    if (UsuarioPerfilExistente == null)
        //        return NotFound(new { mensaje = "El país no existe." });

        //    //Actualizar solo las propiedades del modelo que tienen datos en el DTO
        //    UsuarioPerfilExistente.Id_Usuario = modelo.Id_Usuario ?? UsuarioPerfilExistente.Id_Usuario;
        //    UsuarioPerfilExistente.Id_Perfil = modelo.Id_Perfil ?? UsuarioPerfilExistente.Id_Perfil;

        //    //Realizar la actualización
        //    bool respuesta = await _usuarioPerfilService.Actualizar(UsuarioPerfilExistente);

        //    return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        //}

        // DELETE: api/UsuarioPerfil/5
        [HttpDelete("DeleteUsuarioPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfil(UsuarioPerfilDTO modelo)
        {
            if (modelo == null || modelo.Id_Usuario <= 0 || modelo.Id_Perfil <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    mensaje = "Los datos proporcionados no son válidos. Asegúrese de enviar un usuario y perfil válidos."
                });
            }

            UsuarioPerfil nuevoModelo = new UsuarioPerfil
            {
                Id_Usuario = modelo.Id_Usuario,
                Id_Perfil = modelo.Id_Perfil
            };

            try
            {
                int? usuarioPerfilId = await _usuarioPerfilService.ObtenerId(nuevoModelo);

                if (usuarioPerfilId == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        mensaje = "No se encontró un usuario-perfil con los datos proporcionados."
                    });
                }

                bool eliminado = await _usuarioPerfilService.Eliminar(usuarioPerfilId.Value);

                if (!eliminado)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        mensaje = "Ocurrió un problema al intentar eliminar el usuario-perfil."
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "El usuario-perfil se eliminó correctamente.",
                    id = usuarioPerfilId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un error inesperado al procesar la solicitud.",
                    detalle = ex.Message
                });
            }
        }

        // DELETE: api/DeleteUsuarioPerfilByUsuario/5
        [HttpDelete("DeleteUsuarioPerfilByUsuario")]
        public async Task<IActionResult> DeleteUsuarioPerfilByUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    valor = idUsuario,
                    mensaje = "El ID de usuario debe ser un número positivo."
                });
            }

            try
            {
                bool eliminado = await _usuarioPerfilService.EliminarPorUsuario(idUsuario);

                if (!eliminado)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        valor = idUsuario,
                        mensaje = $"No se encontraron perfiles asociados al usuario con ID {idUsuario}."
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = idUsuario,
                    mensaje = "Todos los perfiles del usuario han sido eliminados correctamente."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    valor = idUsuario,
                    mensaje = "Ocurrió un error inesperado al procesar la solicitud.",
                    detalle = ex.Message
                });
            }
        }

        // DELETE: api/DeleteUsuarioPerfilByPerfil/5
        [HttpDelete("DeleteUsuarioPerfilByPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfilByPerfil(int idPerfil)
        {

            try
            {
                bool eliminado = await _usuarioPerfilService.EliminarPorPerfil(idPerfil);

                if (!eliminado)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { valor = idPerfil, mensaje = $"No se encontraron perfiles asociados al perfil con ID {idPerfil}." });
                }

                return StatusCode(StatusCodes.Status200OK, new { valor = idPerfil, mensaje = "Todos los perfiles del perfil han sido eliminados correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = idPerfil, mensaje = "Error al eliminar los perfiles.", detalle = ex.Message });
            }
        }

        // POST: api/UsuarioPerfil
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUsuarioPerfil")]
        public async Task<IActionResult> PostUsuarioPerfil(UsuarioPerfilDTO modelo)
        {

            try
            {
                UsuarioPerfil NuevoModelo = new UsuarioPerfil
                {
                    Id_Usuario = modelo.Id_Usuario,
                    Id_Perfil = modelo.Id_Perfil
                };

                bool respuesta = await _usuarioPerfilService.Insertar(NuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo insertar el perfil de usuario." });
                }

                return StatusCode(StatusCodes.Status201Created, new { valor = modelo, mensaje = "Perfil de usuario creado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al crear el perfil de usuario.", detalle = ex.Message });
            }
        }

    }
}
