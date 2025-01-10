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
    public class TipoMensajeService:ITipoMensajeService
    {
        private readonly ITipoMensajeRepository<TipoMensaje> _tipoMensajeRepository;
        public TipoMensajeService(ITipoMensajeRepository<TipoMensaje> tipoMensajeRepository)
        {
            _tipoMensajeRepository = tipoMensajeRepository;
        }
        public async Task<bool> Actualizar(TipoMensaje modelo)
        {
            return await _tipoMensajeRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _tipoMensajeRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(TipoMensaje modelo)
        {
            return await _tipoMensajeRepository.Insertar(modelo);
        }

        public async Task<TipoMensaje> ObtenerPorId(int id)
        {
            return await _tipoMensajeRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<TipoMensaje>> ObtenerTodos()
        {
            return await _tipoMensajeRepository.ObtenerTodos();
        }
    }
}
