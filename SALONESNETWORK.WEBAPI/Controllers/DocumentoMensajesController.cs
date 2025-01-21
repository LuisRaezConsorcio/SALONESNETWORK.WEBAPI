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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Lista de documentos de mensajes obtenida correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener los documentos de mensajes.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento de mensaje no fue encontrado.", Resultado = false });
                }

                var DocumentoMensajeDTO = new DocumentoMensajeDTO
                {
                    Id = DocumentoMensaje.Id,
                    Id_Mensaje = DocumentoMensaje.Id_Mensaje,
                    Id_Documento = DocumentoMensaje.Id_Documento
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento de mensaje obtenido correctamente.", Datos = DocumentoMensajeDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el documento de mensaje por ID.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento de mensaje no existe.", Resultado = false });
                }

                DocumentoMensajeExistente.Id_Mensaje = modelo.Id_Mensaje ?? DocumentoMensajeExistente.Id_Mensaje;
                DocumentoMensajeExistente.Id_Documento = modelo.Id_Documento ?? DocumentoMensajeExistente.Id_Documento;

                bool respuesta = await _documentoMensajeService.Actualizar(DocumentoMensajeExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento de mensaje actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar el documento de mensaje.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento de mensaje creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al crear el documento de mensaje.", Error = ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El documento de mensaje no fue encontrado.", Resultado = false });
                }

                bool respuesta = await _documentoMensajeService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Documento de mensaje eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el documento de mensaje.", Error = ex.Message });
            }
        }

    }
}
