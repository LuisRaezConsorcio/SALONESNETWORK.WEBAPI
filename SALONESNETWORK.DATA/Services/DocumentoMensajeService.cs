using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Services
{
    public class DocumentoMensajeService:IDocumentoMensajeService
    {
        private readonly IDocumentoMensajeRepository<DocumentoMensaje> _documentoMensajeRepository;
        public DocumentoMensajeService(IDocumentoMensajeRepository<DocumentoMensaje> documentoMensajeRepository)
        {
            _documentoMensajeRepository = documentoMensajeRepository;
        }
        public async Task<bool> Actualizar(DocumentoMensaje modelo)
        {
            return await _documentoMensajeRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _documentoMensajeRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(DocumentoMensaje modelo)
        {
            return await _documentoMensajeRepository.Insertar(modelo);
        }

        public async Task<DocumentoMensaje> ObtenerPorId(int id)
        {
            return await _documentoMensajeRepository.ObtenerPorId(id);
        }


        public async Task<IQueryable<DocumentoMensaje>> ObtenerTodos()
        {
            return await _documentoMensajeRepository.ObtenerTodos();
        }
    }
}
