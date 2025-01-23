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
    public class UsuarioPerfilesController : ControllerBase
    {

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

                return ResponseHelper.Success(lista, "Lista de Perfiles obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("No hay un Usuario con el Perfil consultado.");
                }

                return ResponseHelper.Success(NuevoModelo, "Registro obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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

                return ResponseHelper.Success(lista, "Perfiles obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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

                return ResponseHelper.Success(lista, "Perfiles obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/UsuarioPerfil/5
        [HttpDelete("DeleteUsuarioPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfil(UsuarioPerfilDTO modelo)
        {
            if (modelo == null || modelo.Id_Usuario <= 0 || modelo.Id_Perfil <= 0)
            {
                return ResponseHelper.BadRequestResponse("Los datos proporcionados no son válidos. Asegúrese de enviar un usuario y perfil válidos.");
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
                    return ResponseHelper.NotFoundResponse("No se encontró un usuario-perfil con los datos proporcionados.");
                }

                bool respuesta = await _usuarioPerfilService.Eliminar(usuarioPerfilId.Value);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("Ocurrió un problema al intentar eliminar el usuario-perfil");
                }

                return ResponseHelper.Success("El usuario-perfil se eliminó correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/DeleteUsuarioPerfilByUsuario/5
        [HttpDelete("DeleteUsuarioPerfilByUsuario")]
        public async Task<IActionResult> DeleteUsuarioPerfilByUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return ResponseHelper.BadRequestResponse("El ID de usuario debe ser un número positivo.");
            }

            try
            {
                bool respuesta = await _usuarioPerfilService.EliminarPorUsuario(idUsuario);

                if (!respuesta)
                {
                    return ResponseHelper.NotFoundResponse($"No se encontraron perfiles asociados al usuario con ID {idUsuario}.");
                }

                return ResponseHelper.Success($"Todos los perfiles del usuario {idUsuario} han sido eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse($"No se encontraron perfiles asociados al perfil con ID {idPerfil}.");
                }

                return ResponseHelper.Success("Todos los perfiles del perfil han sido eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el perfil porque el usuario ya cuenta con él.");
                }

                return ResponseHelper.Success("Perfil de usuario creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

    }
}
