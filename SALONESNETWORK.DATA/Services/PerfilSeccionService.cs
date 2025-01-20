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
    public class PerfilSeccionService:IPerfilSeccionService
    {
        private readonly IPerfilSeccionRepository<PerfilSeccion> _perfilSeccionRepository;
        public PerfilSeccionService(IPerfilSeccionRepository<PerfilSeccion> perfilSeccionRepository)
        {
            _perfilSeccionRepository = perfilSeccionRepository;
        }
        public async Task<bool> Actualizar(PerfilSeccion modelo)
        {
            return await _perfilSeccionRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _perfilSeccionRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(PerfilSeccion modelo)
        {
            return await _perfilSeccionRepository.Insertar(modelo);
        }

        public async Task<PerfilSeccion> ObtenerPorId(int id)
        {
            return await _perfilSeccionRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<PerfilSeccion>> ObtenerTodos()
        {
            return await _perfilSeccionRepository.ObtenerTodos();
        }
    }
}
