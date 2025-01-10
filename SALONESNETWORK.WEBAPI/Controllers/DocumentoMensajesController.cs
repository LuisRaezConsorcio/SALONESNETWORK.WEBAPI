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
    public class DocumentoMensajesController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IDocumentoMensajeService _documentoMensajeService;

        public DocumentoMensajesController(IDocumentoMensajeService documentoMensajeService)
        {
            _documentoMensajeService = documentoMensajeService;
        }

        // GET: api/DocumentoMensaje
        [HttpGet("GetDocumentoMensajes")]
        public async Task<ActionResult<IEnumerable<DocumentoMensaje>>> GetDocumentoMensajes()
        {
            //return await _context.DocumentoMensajees.ToListAsync();
            IQueryable<DocumentoMensaje> queryContactoSQL = await _documentoMensajeService.ObtenerTodos();

            List<DocumentoMensajeDTO> lista = queryContactoSQL
                                                     .Select(c => new DocumentoMensajeDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Mensaje = c.Id_Mensaje,
                                                         Id_Documento = c.Id_Documento
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/DocumentoMensaje/5
        [HttpGet("GetDocumentoMensajeById")]
        public async Task<ActionResult<DocumentoMensaje>> GetDocumentoMensajeById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var DocumentoMensaje = await _documentoMensajeService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (DocumentoMensaje == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var DocumentoMensajeDTO = new DocumentoMensajeDTO
            {
                Id = DocumentoMensaje.Id,
                Id_Mensaje = DocumentoMensaje.Id_Mensaje,
                Id_Documento = DocumentoMensaje.Id_Documento
            };

            // Retorna el DTO con un status 200
            return Ok(DocumentoMensajeDTO);
        }

        // PUT: api/DocumentoMensaje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutDocumentoMensaje")]
        public async Task<IActionResult> PutDocumentoMensaje(DocumentoMensajeDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var DocumentoMensajeExistente = await _documentoMensajeService.ObtenerPorId(modelo.Id);

            if (DocumentoMensajeExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            DocumentoMensajeExistente.Id_Mensaje = modelo.Id_Mensaje ?? DocumentoMensajeExistente.Id_Mensaje;
            DocumentoMensajeExistente.Id_Documento = modelo.Id_Documento ?? DocumentoMensajeExistente.Id_Documento;

            // Realizar la actualización
            bool respuesta = await _documentoMensajeService.Actualizar(DocumentoMensajeExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/DocumentoMensaje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostDocumentoMensaje")]
        public async Task<IActionResult> PostDocumentoMensaje(DocumentoMensajeDTO modelo)
        {

            DocumentoMensaje NuevoModelo = new DocumentoMensaje()
            {
                Id_Mensaje = modelo.Id_Mensaje,
                Id_Documento = modelo.Id_Documento
            };

            bool respuesta = await _documentoMensajeService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/DocumentoMensaje/5
        [HttpDelete("DeleteDocumentoMensaje")]
        public async Task<IActionResult> DeleteDocumentoMensaje(int id)
        {
            var DocumentoMensaje = await _documentoMensajeService.ObtenerPorId(id);
            if (DocumentoMensaje == null)
            {
                return NotFound();
            }

            await _documentoMensajeService.Eliminar(id);
            //await _documentoMensajeService.SaveChangesAsync();

            return NoContent();
        }

        //private bool DocumentoMensajeExists(int id)
        //{
        //    return _context.DocumentoMensajees.Any(e => e.Id == id);
        //}
    }
}
