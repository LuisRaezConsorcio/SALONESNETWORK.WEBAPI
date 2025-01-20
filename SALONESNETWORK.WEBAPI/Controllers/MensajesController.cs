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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IMensajeService _mensajeService;

        public MensajesController(IMensajeService mensajeService)
        {
            _mensajeService = mensajeService;
        }

        // GET: api/Mensaje
        [HttpGet("GetMensajes")]
        public async Task<ActionResult<IEnumerable<Mensaje>>> GetMensajes()
        {
            //return await _context.Mensajees.ToListAsync();
            IQueryable<Mensaje> queryContactoSQL = await _mensajeService.ObtenerTodos();

            List<MensajeDTO> lista = queryContactoSQL
                                                     .Select(c => new MensajeDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_TipoMensaje = c.Id_TipoMensaje,
                                                         Id_Usuario = c.Id_Usuario,
                                                         Nombre = c.Nombre,
                                                         Descripcion = c.Descripcion,
                                                         FechaCreacion = c.FechaCreacion,
                                                         UsuarioCreacion = c.UsuarioCreacion,
                                                         FechaModificacion = c.FechaModificacion,
                                                         UsuarioModificacion = c.UsuarioModificacion,
                                                         Seguimiento = c.Seguimiento,
                                                         Id_MensajeSeguimiento = c.Id_MensajeSeguimiento,
                                                         Respuesta = c.Respuesta,
                                                         Id_MensajeRespuesta = c.Id_MensajeRespuesta
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Mensaje/5
        [HttpGet("GetMensajeById")]
        public async Task<ActionResult<Mensaje>> GetMensajeById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Mensaje = await _mensajeService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Mensaje == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var MensajeDTO = new MensajeDTO
            {
                Id = Mensaje.Id,
                Id_TipoMensaje = Mensaje.Id_TipoMensaje,
                Id_Usuario = Mensaje.Id_Usuario,
                Nombre = Mensaje.Nombre,
                Descripcion = Mensaje.Descripcion,
                FechaCreacion = Mensaje.FechaCreacion,
                UsuarioCreacion = Mensaje.UsuarioCreacion,
                FechaModificacion = Mensaje.FechaModificacion,
                UsuarioModificacion = Mensaje.UsuarioModificacion,
                Seguimiento = Mensaje.Seguimiento,
                Id_MensajeSeguimiento = Mensaje.Id_MensajeSeguimiento,
                Respuesta = Mensaje.Respuesta,
                Id_MensajeRespuesta = Mensaje.Id_MensajeRespuesta
            };

            // Retorna el DTO con un status 200
            return Ok(MensajeDTO);
        }

        // PUT: api/Mensaje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutMensaje")]
        public async Task<IActionResult> PutMensaje(MensajeDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var MensajeExistente = await _mensajeService.ObtenerPorId(modelo.Id);

            if (MensajeExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            MensajeExistente.Id_TipoMensaje = modelo.Id_TipoMensaje ?? MensajeExistente.Id_TipoMensaje;
            MensajeExistente.Id_Usuario = modelo.Id_Usuario ?? MensajeExistente.Id_Usuario;
            MensajeExistente.Nombre = modelo.Nombre ?? MensajeExistente.Nombre;
            MensajeExistente.Descripcion = modelo.Descripcion ?? MensajeExistente.Descripcion;
            MensajeExistente.FechaModificacion = DateTime.Now;
            MensajeExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? MensajeExistente.UsuarioModificacion;
            MensajeExistente.Seguimiento = modelo.Seguimiento ?? MensajeExistente.Seguimiento;
            MensajeExistente.Id_MensajeSeguimiento = modelo.Id_MensajeSeguimiento ?? MensajeExistente.Id_MensajeSeguimiento;
            MensajeExistente.Respuesta = modelo.Respuesta ?? MensajeExistente.Respuesta;
            MensajeExistente.Id_MensajeRespuesta = modelo.Id_MensajeRespuesta ?? MensajeExistente.Id_MensajeRespuesta;

            // Realizar la actualización
            bool respuesta = await _mensajeService.Actualizar(MensajeExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Mensaje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostMensaje")]
        public async Task<IActionResult> PostMensaje(MensajeDTO modelo)
        {

            Mensaje NuevoModelo = new Mensaje()
            {
                
                Id_TipoMensaje = modelo.Id_TipoMensaje,
                Id_Usuario = modelo.Id_Usuario,
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = 1,
                Seguimiento = modelo.Seguimiento,
                Id_MensajeSeguimiento = modelo.Id_MensajeSeguimiento,
                Respuesta = modelo.Respuesta,
                Id_MensajeRespuesta = modelo.Id_MensajeRespuesta
            };

            bool respuesta = await _mensajeService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Mensaje/5
        [HttpDelete("DeleteMensaje")]
        public async Task<IActionResult> DeleteMensaje(int id)
        {
            var Mensaje = await _mensajeService.ObtenerPorId(id);
            if (Mensaje == null)
            {
                return NotFound();
            }

            await _mensajeService.Eliminar(id);
            //await _mensajeService.SaveChangesAsync();

            return NoContent();
        }

        //private bool MensajeExists(int id)
        //{
        //    return _context.Mensajees.Any(e => e.Id == id);
        //}
    }
}
