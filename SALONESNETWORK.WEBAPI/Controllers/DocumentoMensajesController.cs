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
    public class DocumentoMensajesController : ControllerBase
    {

        private readonly IDocumentoMensajeService _documentoMensajeService;

        public DocumentoMensajesController(IDocumentoMensajeService documentoMensajeService)
        {
            _documentoMensajeService = documentoMensajeService;
        }

        // GET: api/DocumentoMensaje
        [HttpGet("GetDocumentoMensajes")]
        public async Task<IActionResult> GetDocumentoMensajes()
        {
            try
            {
                IQueryable<DocumentoMensaje> queryContactoSQL = await _documentoMensajeService.ObtenerTodos();

                List<DocumentoMensajeDTO> lista = queryContactoSQL
                    .Select(c => new DocumentoMensajeDTO
                    {
                        Id = c.Id,
                        Id_Mensaje = c.Id_Mensaje,
                        Id_Documento = c.Id_Documento
                    }).ToList();

                return ResponseHelper.Success(lista, "Lista de documentos de mensajes obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/DocumentoMensaje/5
        [HttpGet("GetDocumentoMensajeById")]
        public async Task<IActionResult> GetDocumentoMensajeById(int id)
        {
            try
            {
                var DocumentoMensaje = await _documentoMensajeService.ObtenerPorId(id);

                if (DocumentoMensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El documento de mensaje no fue encontrado.");
                }

                var DocumentoMensajeDTO = new DocumentoMensajeDTO
                {
                    Id = DocumentoMensaje.Id,
                    Id_Mensaje = DocumentoMensaje.Id_Mensaje,
                    Id_Documento = DocumentoMensaje.Id_Documento
                };

                return ResponseHelper.Success(DocumentoMensajeDTO, "Documento de mensaje obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/DocumentoMensaje/5
        [HttpPut("PutDocumentoMensaje")]
        public async Task<IActionResult> PutDocumentoMensaje(DocumentoMensajeDTO modelo)
        {
            try
            {
                var DocumentoMensajeExistente = await _documentoMensajeService.ObtenerPorId(modelo.Id);

                if (DocumentoMensajeExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("El documento de mensaje no existe.");
                }

                DocumentoMensajeExistente.Id_Mensaje = modelo.Id_Mensaje ?? DocumentoMensajeExistente.Id_Mensaje;
                DocumentoMensajeExistente.Id_Documento = modelo.Id_Documento ?? DocumentoMensajeExistente.Id_Documento;

                bool respuesta = await _documentoMensajeService.Actualizar(DocumentoMensajeExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el registro.");
                }

                return ResponseHelper.Success("Documento de mensaje actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/DocumentoMensaje
        [HttpPost("PostDocumentoMensaje")]
        public async Task<IActionResult> PostDocumentoMensaje(DocumentoMensajeDTO modelo)
        {
            try
            {
                DocumentoMensaje nuevoModelo = new DocumentoMensaje
                {
                    Id_Mensaje = modelo.Id_Mensaje,
                    Id_Documento = modelo.Id_Documento
                };

                bool respuesta = await _documentoMensajeService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el registro.");
                }

                return ResponseHelper.Success("Documento de mensaje creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/DocumentoMensaje/5
        [HttpDelete("DeleteDocumentoMensaje")]
        public async Task<IActionResult> DeleteDocumentoMensaje(int id)
        {
            try
            {
                var DocumentoMensaje = await _documentoMensajeService.ObtenerPorId(id);

                if (DocumentoMensaje == null)
                {
                    return ResponseHelper.NotFoundResponse("El documento de mensaje no fue encontrado.");
                }

                bool respuesta = await _documentoMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el registro.");
                }

                return ResponseHelper.Success("Documento de mensaje eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

    }
}
