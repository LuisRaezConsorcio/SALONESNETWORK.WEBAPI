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
        [HttpGet("GetUsuarioPerfiles")]
        public async Task<ActionResult<IEnumerable<UsuarioPerfil>>> GetUsuarioPerfiles()
        {
            //return await _context.UsuarioPerfiles.ToListAsync();
            IQueryable<UsuarioPerfil> queryContactoSQL = await _usuarioPerfilService.ObtenerTodos();

            List<UsuarioPerfilDTO> lista = queryContactoSQL
                                                     .Select(c => new UsuarioPerfilDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Usuario = c.Id_Usuario,
                                                         Id_Perfil = c.Id_Perfil
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/UsuarioPerfil/5
        [HttpGet("GetUsuarioPerfilById")]
        public async Task<ActionResult<UsuarioPerfil>> GetUsuarioPerfilById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var UsuarioPerfil = await _usuarioPerfilService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (UsuarioPerfil == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var UsuarioPerfilDTO = new UsuarioPerfilDTO
            {
                Id = UsuarioPerfil.Id,
                Id_Usuario = UsuarioPerfil.Id_Usuario,
                Id_Perfil = UsuarioPerfil.Id_Perfil
            };

            // Retorna el DTO con un status 200
            return Ok(UsuarioPerfilDTO);
        }

        // PUT: api/UsuarioPerfil/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUsuarioPerfil")]
        public async Task<IActionResult> PutUsuarioPerfil(UsuarioPerfilDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var UsuarioPerfilExistente = await _usuarioPerfilService.ObtenerPorId(modelo.Id);

            if (UsuarioPerfilExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            UsuarioPerfilExistente.Id_Usuario = modelo.Id_Usuario ?? UsuarioPerfilExistente.Id_Usuario;
            UsuarioPerfilExistente.Id_Perfil = modelo.Id_Perfil ?? UsuarioPerfilExistente.Id_Perfil;

            // Realizar la actualización
            bool respuesta = await _usuarioPerfilService.Actualizar(UsuarioPerfilExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/UsuarioPerfil
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostUsuarioPerfil")]
        public async Task<IActionResult> PostUsuarioPerfil(UsuarioPerfilDTO modelo)
        {

            UsuarioPerfil NuevoModelo = new UsuarioPerfil()
            {
                Id_Usuario = modelo.Id_Usuario,
                Id_Perfil = modelo.Id_Perfil
            };

            bool respuesta = await _usuarioPerfilService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/UsuarioPerfil/5
        [HttpDelete("DeleteUsuarioPerfil")]
        public async Task<IActionResult> DeleteUsuarioPerfil(int id)
        {
            var UsuarioPerfil = await _usuarioPerfilService.ObtenerPorId(id);
            if (UsuarioPerfil == null)
            {
                return NotFound();
            }

            await _usuarioPerfilService.Eliminar(id);
            //await _usuarioPerfilService.SaveChangesAsync();

            return NoContent();
        }

        //private bool UsuarioPerfilExists(int id)
        //{
        //    return _context.UsuarioPerfiles.Any(e => e.Id == id);
        //}
    }
}
