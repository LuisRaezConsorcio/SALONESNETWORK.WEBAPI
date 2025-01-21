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
        public async Task<IActionResult> GetDocumentos()
        {
            try
            {
                IQueryable<Documento> query = await _documentosService.ObtenerTodos();

                List<DocumentoDTO> lista = query
                    .Select(c => new DocumentoDTO
                    {
                        Id = c.Id,
                        Descripcion = c.Descripcion,
                        Ubicacion = c.Ubicacion
                    }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Lista de documentos obtenida correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener los documentos.", Error = ex.Message });
            }
        }

        // GET: api/Documentos/5
        [HttpGet("GetDocumentosById")]
        public async Task<IActionResult> GetDocumentosById(int id)
        {
            try
            {
                var documento = await _documentosService.ObtenerPorId(id);

                if (documento == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento no fue encontrado.", Resultado = false });
                }

                var documentoDTO = new DocumentoDTO
                {
                    Id = documento.Id,
                    Descripcion = documento.Descripcion,
                    Ubicacion = documento.Ubicacion
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento obtenido correctamente.", Datos = documentoDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el documento por ID.", Error = ex.Message });
            }
        }

        // PUT: api/Documentos/5
        [HttpPut("PutDocumentos")]
        public async Task<IActionResult> PutDocumentos(DocumentoDTO modelo)
        {
            try
            {
                var documentoExistente = await _documentosService.ObtenerPorId(modelo.Id);

                if (documentoExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento no existe.", Resultado = false });
                }

                documentoExistente.Descripcion = modelo.Descripcion;
                documentoExistente.Ubicacion = modelo.Ubicacion;

                bool respuesta = await _documentosService.Actualizar(documentoExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el documento.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar el documento.", Error = ex.Message });
            }
        }

        // POST: api/Documentos
        [HttpPost("PostDocumentos")]
        public async Task<IActionResult> PostDocumentos(DocumentoDTO modelo)
        {
            try
            {
                Documento nuevoDocumento = new Documento
                {
                    Descripcion = modelo.Descripcion,
                    Ubicacion = modelo.Ubicacion
                };

                bool respuesta = await _documentosService.Insertar(nuevoDocumento);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el documento", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al crear el documento.", Error = ex.Message });
            }
        }

        // DELETE: api/Documentos/5
        [HttpDelete("DeleteDocumentos")]
        public async Task<IActionResult> DeleteDocumentos(int id)
        {
            try
            {
                var documento = await _documentosService.ObtenerPorId(id);

                if (documento == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _documentosService.Eliminar(id);

                if (!respuesta)
                {
                   return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el documento.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el documento.", Error = ex.Message });
            }
        }
    }
}
