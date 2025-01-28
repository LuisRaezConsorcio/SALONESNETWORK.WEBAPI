using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.WEBAPI.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;
using SALONESNETWORK.BLL.Helpers;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {

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

                return ResponseHelper.Success(lista, "Lista de documentos obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El documento no fue encontrado.");
                }

                var documentoDTO = new DocumentoDTO
                {
                    Id = documento.Id,
                    Descripcion = documento.Descripcion,
                    Ubicacion = documento.Ubicacion
                };

                return ResponseHelper.Success(documentoDTO, "Documento obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El documento no fue encontrado.");
                }

                documentoExistente.Descripcion = modelo.Descripcion;
                documentoExistente.Ubicacion = modelo.Ubicacion;

                bool respuesta = await _documentosService.Actualizar(documentoExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el documento.");
                }

                return ResponseHelper.Success("Documento actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el documento.");
                }

                return ResponseHelper.Success("Documento creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El documento no fue encontrado.");
                }

                bool respuesta = await _documentosService.Eliminar(id);

                if (!respuesta)
                {
                   return ResponseHelper.BadRequestResponse("No se pudo eliminar el documento.");
                }

                return ResponseHelper.Success("Documento eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
