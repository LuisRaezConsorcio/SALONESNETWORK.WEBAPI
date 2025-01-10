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
    public class DocumentoService:IDocumentoService
    {
        private readonly IDocumentoRepository<Documento> _documentoRepository;
        public DocumentoService(IDocumentoRepository<Documento> documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }
        public async Task<bool> Actualizar(Documento modelo)
        {
            return await _documentoRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _documentoRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Documento modelo)
        {
            return await _documentoRepository.Insertar(modelo);
        }

        public async Task<Documento> ObtenerPorId(int id)
        {
            return await _documentoRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<Documento>> ObtenerTodos()
        {
            return await _documentoRepository.ObtenerTodos();
        }
    }
}
