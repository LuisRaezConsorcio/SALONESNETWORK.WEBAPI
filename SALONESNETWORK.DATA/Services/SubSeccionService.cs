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
    public class SubSeccionService:ISubSeccionService
    {
        private readonly ISubSeccionRepository<SubSeccion> _subSeccionRepository;
        public SubSeccionService(ISubSeccionRepository<SubSeccion> subSeccionRepository)
        {
            _subSeccionRepository = subSeccionRepository;
        }
        public async Task<bool> Actualizar(SubSeccion modelo)
        {
            return await _subSeccionRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _subSeccionRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(SubSeccion modelo)
        {
            return await _subSeccionRepository.Insertar(modelo);
        }

        public async Task<SubSeccion> ObtenerPorId(int id)
        {
            return await _subSeccionRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<SubSeccion>> ObtenerTodos()
        {
            return await _subSeccionRepository.ObtenerTodos();
        }
    }
}
