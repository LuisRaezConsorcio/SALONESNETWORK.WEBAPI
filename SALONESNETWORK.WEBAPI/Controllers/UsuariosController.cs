using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using SALONESNETWORK.BLL.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                IQueryable<Usuario> queryContactoSQL = await _usuarioService.ObtenerTodos();
                var lista = queryContactoSQL.Select(c => new UsuarioDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    UserName = c.UserName,
                    EmployedId = c.EmployedId,
                    LocalTypeId = c.LocalTypeId,
                    LocalId = c.LocalId,
                    AreaId = c.AreaId,
                    UserLocalId = c.UserLocalId,
                    UserLocalId2 = c.UserLocalId2,
                    UserLocalId3 = c.UserLocalId3,
                    UserLocalComercialId = c.UserLocalComercialId,
                    Token = c.Token,
                    Estado = c.Estado
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Usuarios obtenidos correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los usuarios.", Error = ex.Message });
            }
        }

        // GET: api/Usuario/5
        [HttpGet("GetUsuarioById")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerPorId(id);
                if (usuario == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El usuario no fue encontrado.", Resultado = false });
                }

                var usuarioDTO = new UsuarioDTO
                {
                    Id = usuario.Id,
                    FirstName = usuario.FirstName,
                    LastName = usuario.LastName,
                    UserName = usuario.UserName,
                    EmployedId = usuario.EmployedId,
                    LocalTypeId = usuario.LocalTypeId,
                    LocalId = usuario.LocalId,
                    AreaId = usuario.AreaId,
                    UserLocalId = usuario.UserLocalId,
                    UserLocalId2 = usuario.UserLocalId2,
                    UserLocalId3 = usuario.UserLocalId3,
                    UserLocalComercialId = usuario.UserLocalComercialId,
                    Token = usuario.Token,
                    Estado = usuario.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Usuario obtenido correctamente.", Datos = usuarioDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener el usuario.", Error = ex.Message });
            }
        }

        // PUT: api/Usuario/5
        [HttpPut("PutUsuario")]
        public async Task<IActionResult> PutUsuario(UsuarioDTO modelo)
        {
            try
            {
                var usuarioExistente = await _usuarioService.ObtenerPorId(modelo.Id);
                if (usuarioExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El usuario no existe." });
                }

                usuarioExistente.FirstName = modelo.FirstName ?? usuarioExistente.FirstName;
                usuarioExistente.LastName = modelo.LastName ?? usuarioExistente.LastName;
                usuarioExistente.UserName = modelo.UserName ?? usuarioExistente.UserName;
                usuarioExistente.EmployedId = modelo.EmployedId ?? usuarioExistente.EmployedId;
                usuarioExistente.LocalTypeId = modelo.LocalTypeId ?? usuarioExistente.LocalTypeId;
                usuarioExistente.LocalId = modelo.LocalId ?? usuarioExistente.LocalId;
                usuarioExistente.AreaId = modelo.AreaId ?? usuarioExistente.AreaId;
                usuarioExistente.UserLocalId = modelo.UserLocalId ?? usuarioExistente.UserLocalId;
                usuarioExistente.UserLocalId2 = modelo.UserLocalId2 ?? usuarioExistente.UserLocalId2;
                usuarioExistente.UserLocalId3 = modelo.UserLocalId3 ?? usuarioExistente.UserLocalId3;
                usuarioExistente.UserLocalComercialId = modelo.UserLocalComercialId ?? usuarioExistente.UserLocalComercialId;
                usuarioExistente.Token = modelo.Token ?? usuarioExistente.Token;
                usuarioExistente.Estado = modelo.Estado ?? usuarioExistente.Estado;

                bool respuesta = await _usuarioService.Actualizar(usuarioExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el usuario.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Usuario actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar el usuario.", Error = ex.Message });
            }
        }

        // POST: api/Usuario
        [HttpPost("PostUsuario")]
        public async Task<IActionResult> PostUsuario(UsuarioDTO modelo)
        {
            try
            {
                Usuario nuevoModelo = new Usuario
                {
                    FirstName = modelo.FirstName,
                    LastName = modelo.LastName,
                    UserName = modelo.UserName,
                    EmployedId = modelo.EmployedId,
                    LocalTypeId = modelo.LocalTypeId,
                    LocalId = modelo.LocalId,
                    AreaId = modelo.AreaId,
                    UserLocalId = modelo.UserLocalId,
                    UserLocalId2 = modelo.UserLocalId2,
                    UserLocalId3 = modelo.UserLocalId3,
                    UserLocalComercialId = modelo.UserLocalComercialId,
                    Token = modelo.Token,
                    Estado = true
                };

                bool respuesta = await _usuarioService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el usuario.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Usuario creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear el usuario.", Error = ex.Message });
            }

        }

        // DELETE: api/Usuario/5
        [HttpDelete("DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerPorId(id);
                if (usuario == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El usuario no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _usuarioService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el usuario.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Usuario eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar el usuario.", Error = ex.Message });
            }
        }
    }
}
