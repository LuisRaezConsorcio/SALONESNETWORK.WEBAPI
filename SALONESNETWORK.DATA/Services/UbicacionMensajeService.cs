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
    public class UbicacionMensajeService : IUbicacionMensajeService
    {
        private readonly IUbicacionMensajeRepository<UbicacionMensaje> _ubicacionMensajeRepository;
        public UbicacionMensajeService(IUbicacionMensajeRepository<UbicacionMensaje> ubicacionMensajeRepository)
        {
            _ubicacionMensajeRepository = ubicacionMensajeRepository;
        }
        public async Task<bool> Actualizar(UbicacionMensaje modelo)
        {
            return await _ubicacionMensajeRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _ubicacionMensajeRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(UbicacionMensaje modelo)
        {
            return await _ubicacionMensajeRepository.Insertar(modelo);
        }

        public async Task<UbicacionMensaje> ObtenerPorId(int id)
        {
            return await _ubicacionMensajeRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<UbicacionMensaje>> ObtenerTodos()
        {
            return await _ubicacionMensajeRepository.ObtenerTodos();
        }
    }
}
