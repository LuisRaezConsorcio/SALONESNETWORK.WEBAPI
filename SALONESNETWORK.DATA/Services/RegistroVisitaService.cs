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
    public class RegistroVisitaService:IRegistroVisitaService
    {
        private readonly IRegistroVisitaRepository<RegistroVisita> _RegistroVisitaRepository;
        public RegistroVisitaService(IRegistroVisitaRepository<RegistroVisita> RegistroVisitaRepository)
        {
            _RegistroVisitaRepository = RegistroVisitaRepository;
        }
        public async Task<bool> Actualizar(RegistroVisita modelo)
        {
            return await _RegistroVisitaRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _RegistroVisitaRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(RegistroVisita modelo)
        {
            return await _RegistroVisitaRepository.Insertar(modelo);
        }

        public async Task<RegistroVisita> ObtenerPorId(int id)
        {
            return await _RegistroVisitaRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<RegistroVisita>> ObtenerTodos()
        {
            return await _RegistroVisitaRepository.ObtenerTodos();
        }
    }
}
