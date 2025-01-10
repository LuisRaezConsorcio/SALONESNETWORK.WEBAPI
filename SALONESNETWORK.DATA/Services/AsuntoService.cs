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
    public class AsuntoService:IAsuntoService
    {
        private readonly IAsuntoRepository<Asunto> _asuntoRepository;
        public AsuntoService(IAsuntoRepository<Asunto> asuntoRepository)
        {
            _asuntoRepository = asuntoRepository;
        }
        public async Task<bool> Actualizar(Asunto modelo)
        {
            return await _asuntoRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _asuntoRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Asunto modelo)
        {
            return await _asuntoRepository.Insertar(modelo);
        }

        public async Task<Asunto> ObtenerPorId(int id)
        {
            return await _asuntoRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<Asunto>> ObtenerTodos()
        {
            return await _asuntoRepository.ObtenerTodos();
        }
    }
}
