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
    public class SeccionService:ISeccionService
    {
        private readonly ISeccionRepository<Seccion> _SeccionRepository;
        public SeccionService(ISeccionRepository<Seccion> SeccionRepository)
        {
            _SeccionRepository = SeccionRepository;
        }
        public async Task<bool> Actualizar(Seccion modelo)
        {
            return await _SeccionRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _SeccionRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Seccion modelo)
        {
            return await _SeccionRepository.Insertar(modelo);
        }

        public async Task<Seccion> ObtenerPorId(int id)
        {
            return await _SeccionRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<Seccion>> ObtenerTodos()
        {
            return await _SeccionRepository.ObtenerTodos();
        }
    }
}
