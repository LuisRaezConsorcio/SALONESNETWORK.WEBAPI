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
    public class DocumentosController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

        private readonly IDocumentoService _documentosService;

        public DocumentosController(IDocumentoService documentosService)
        {
            _documentosService = documentosService;
        }

        // GET: api/Documentos
        [HttpGet("GetDocumentos")]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumentos()
        {
            //return await _context.Documentoses.ToListAsync();
            IQueryable<Documento> queryContactoSQL = await _documentosService.ObtenerTodos();

            List<DocumentoDTO> lista = queryContactoSQL
                                                     .Select(c => new DocumentoDTO()
                                                     {
                                                         Id = c.Id,
                                                         Descripcion = c.Descripcion,
                                                         Ubicacion = c.Ubicacion
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/Documentos/5
        [HttpGet("GetDocumentosById")]
        public async Task<ActionResult<Documento>> GetDocumentosById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var Documentos = await _documentosService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (Documentos == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var DocumentosDTO = new DocumentoDTO
            {
                Id = Documentos.Id,
                Descripcion = Documentos.Descripcion,
                Ubicacion = Documentos.Ubicacion
            };

            // Retorna el DTO con un status 200
            return Ok(DocumentosDTO);
        }

        // PUT: api/Documentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutDocumentos")]
        public async Task<IActionResult> PutDocumentos(DocumentoDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var DocumentosExistente = await _documentosService.ObtenerPorId(modelo.Id);

            if (DocumentosExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            DocumentosExistente.Descripcion = modelo.Descripcion;
            DocumentosExistente.Ubicacion = modelo.Ubicacion;

            // Realizar la actualización
            bool respuesta = await _documentosService.Actualizar(DocumentosExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/Documentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostDocumentos")]
        public async Task<IActionResult> PostDocumentos(DocumentoDTO modelo)
        {

            Documento NuevoModelo = new Documento()
            {
                Descripcion = modelo.Descripcion,
                Ubicacion = modelo.Ubicacion
            };

            bool respuesta = await _documentosService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/Documentos/5
        [HttpDelete("DeleteDocumentos")]
        public async Task<IActionResult> DeleteDocumentos(int id)
        {
            var Documentos = await _documentosService.ObtenerPorId(id);
            if (Documentos == null)
            {
                return NotFound();
            }

            await _documentosService.Eliminar(id);
            //await _documentosService.SaveChangesAsync();

            return NoContent();
        }

        //private bool DocumentosExists(int id)
        //{
        //    return _context.Documentoses.Any(e => e.Id == id);
        //}
    }
}
