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
                                                         Name = c.Name
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
                Name = Usuario.Name
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
            UsuarioExistente.Name = modelo.Name ?? UsuarioExistente.Name;

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
                Name = modelo.Name
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
