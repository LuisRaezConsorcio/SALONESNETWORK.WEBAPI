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
        //private readonly SalonesDbContext _context;

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            //return await _context.Usuarioes.ToListAsync();
            IQueryable<Usuario> queryContactoSQL = await _usuarioService.ObtenerTodos();

            List<UsuarioDTO> lista = queryContactoSQL
                                                     .Select(c => new UsuarioDTO()
                                                     {
                                                         Id = c.Id,
                                                          FirstName= c.FirstName,
                                                          LastName= c.LastName,
                                                          UserName= c.UserName,
                                                          EmployedId= c.EmployedId,
                                                          LocalTypeId= c.LocalTypeId,
                                                          LocalId= c.LocalId,
                                                          AreaId= c.AreaId,
                                                          UserLocalId= c.UserLocalId,
                                                          UserLocalId2 = c.UserLocalId2,
                                                          UserLocalId3 = c.UserLocalId3,
                                                          UserLocalComercialId= c.UserLocalComercialId,
                                                          Token = c.Token
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Usuario/5
        [HttpGet("GetUsuarioById")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Usuario = await _usuarioService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Usuario == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var UsuarioDTO = new UsuarioDTO
            {
                Id = Usuario.Id,
                FirstName = Usuario.FirstName,
                LastName = Usuario.LastName,
                UserName = Usuario.UserName,
                EmployedId = Usuario.EmployedId,
                LocalTypeId = Usuario.LocalTypeId,
                LocalId = Usuario.LocalId,
                AreaId = Usuario.AreaId,
                UserLocalId = Usuario.UserLocalId,
                UserLocalId2 = Usuario.UserLocalId2,
                UserLocalId3 = Usuario.UserLocalId3,
                UserLocalComercialId = Usuario.UserLocalComercialId,
                Token = Usuario.Token
            };

            // Retorna el DTO con un status 200
            return Ok(UsuarioDTO);
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUsuario")]
        public async Task<IActionResult> PutUsuario(UsuarioDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var UsuarioExistente = await _usuarioService.ObtenerPorId(modelo.Id);

            if (UsuarioExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            UsuarioExistente.FirstName = modelo.FirstName ?? UsuarioExistente.FirstName;
            UsuarioExistente.LastName = modelo.LastName ?? UsuarioExistente.LastName;
            UsuarioExistente.UserName = modelo.UserName ?? UsuarioExistente.UserName;
            UsuarioExistente.EmployedId = modelo.EmployedId ?? UsuarioExistente.EmployedId;
            UsuarioExistente.LocalTypeId = modelo.LocalTypeId ?? UsuarioExistente.LocalTypeId;
            UsuarioExistente.LocalId = modelo.LocalId ?? UsuarioExistente.LocalId;
            UsuarioExistente.AreaId = modelo.AreaId ?? UsuarioExistente.AreaId;
            UsuarioExistente.UserLocalId = modelo.UserLocalId ?? UsuarioExistente.UserLocalId;
            UsuarioExistente.UserLocalId2 = modelo.UserLocalId2 ?? UsuarioExistente.UserLocalId2;
            UsuarioExistente.UserLocalId3 = modelo.UserLocalId3 ?? UsuarioExistente.UserLocalId3;
            UsuarioExistente.UserLocalComercialId = modelo.UserLocalComercialId ?? UsuarioExistente.UserLocalComercialId;
            UsuarioExistente.Token = modelo.Token ?? UsuarioExistente.Token;

            // Realizar la actualización
            bool respuesta = await _usuarioService.Actualizar(UsuarioExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUsuario")]
        public async Task<IActionResult> PostUsuario(UsuarioDTO modelo)
        {

            Usuario NuevoModelo = new Usuario()
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
                Token = modelo.Token
            };

            bool respuesta = await _usuarioService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Usuario/5
        [HttpDelete("DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var Usuario = await _usuarioService.ObtenerPorId(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.Eliminar(id);
            //await _usuarioService.SaveChangesAsync();

            return NoContent();
        }

        //private bool UsuarioExists(int id)
        //{
        //    return _context.Usuarioes.Any(e => e.Id == id);
        //}
    }
}
