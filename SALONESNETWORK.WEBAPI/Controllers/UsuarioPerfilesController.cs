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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Lista obtenida correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener la lista.", Error= ex.Message });
            }
        }

        // GET: api/GetUsuarioPerfilById/5
        [HttpGet("GetUsuarioPerfilIdByUsuarioPerfil")]
        public async Task<IActionResult> GetUsuarioPerfilIdByUsuarioPerfil(UsuarioPerfilDTO modelo)
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "No hay un Usuario con el Perfil consultado.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new {  Mensaje = "Id encontrado.",Datos = UsuarioPerfilId, Resultado = true, });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener el ID.", Error= ex.Message });
            }
        }
        
        // GET: api/UsuarioPerfil/5
        [HttpGet("GetUsuarioPerfilByUsuario")]
        public async Task<IActionResult> GetUsuarioPerfilByUsuario(int idusuario)
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfiles obtenidos correctamente.", Datos = lista, Resultado = lista});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los perfiles.", Error= ex.Message });
            }
        }

        // GET: api/GetUsuarioPerfilByPerfil/5
        [HttpGet("GetUsuarioPerfilByPerfil")]
        public async Task<IActionResult> GetUsuarioPerfilByPerfil(int idperfil)
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Perfiles obtenidos correctamente.", Datos = lista, Resultado = true});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los perfiles.", Error= ex.Message });
            }
        }

        // DELETE: api/UsuarioPerfil/5
        [HttpDelete("DeleteUsuarioPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfil(UsuarioPerfilDTO modelo)
        {
            if (modelo == null || modelo.Id_Usuario <= 0 || modelo.Id_Perfil <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "Los datos proporcionados no son válidos. Asegúrese de enviar un usuario y perfil válidos.", Resultado = false });
            }

            UsuarioPerfil nuevoModelo = new UsuarioPerfil
            {
                Id_Usuario = modelo.Id_Usuario,
                Id_Perfil = modelo.Id_Perfil,
                Estado = modelo.Estado
            };

            try
            {
                int? usuarioPerfilId = await _usuarioPerfilService.ObtenerId(nuevoModelo);

                if (usuarioPerfilId == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "No se encontró un usuario-perfil con los datos proporcionados.", Resultado = false });
                }

                bool respuesta = await _usuarioPerfilService.Eliminar(usuarioPerfilId.Value);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un problema al intentar eliminar el usuario-perfil.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El usuario-perfil se eliminó correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error inesperado al procesar la solicitud.", Error= ex.Message });
            }
        }

        // DELETE: api/DeleteUsuarioPerfilByUsuario/5
        [HttpDelete("DeleteUsuarioPerfilByUsuario")]
        public async Task<IActionResult> DeleteUsuarioPerfilByUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "El ID de usuario debe ser un número positivo.", Resultado = false });
            }

            try
            {
                bool respuesta = await _usuarioPerfilService.EliminarPorUsuario(idUsuario);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = $"No se encontraron perfiles asociados al usuario con ID {idUsuario}.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Todos los perfiles del usuario han sido eliminados correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error inesperado al procesar la solicitud.", Error= ex.Message });
            }
        }

        // DELETE: api/DeleteUsuarioPerfilByPerfil/5
        [HttpDelete("DeleteUsuarioPerfilByPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfilByPerfil(int idPerfil)
        {

            try
            {
                bool respuesta = await _usuarioPerfilService.EliminarPorPerfil(idPerfil);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = $"No se encontraron perfiles asociados al perfil con ID {idPerfil}.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Todos los perfiles del perfil han sido eliminados correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar los perfiles.", Error= ex.Message });
            }
        }

        // POST: api/UsuarioPerfil
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el perfil porque el usuario ya cuenta con él.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Perfil de usuario creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear el perfil de usuario.", Error= ex.Message });
            }
        }

    }
}
