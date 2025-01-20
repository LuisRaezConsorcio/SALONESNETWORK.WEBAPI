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
    public class AsuntoPaisSeccionSubService:IAsuntoPaisSeccionSubService
    {
        private readonly IAsuntoPaisSeccionSubRepository<AsuntoPaisSeccionSub> _asuntoPaisSeccionSubRepository;
        public AsuntoPaisSeccionSubService(IAsuntoPaisSeccionSubRepository<AsuntoPaisSeccionSub> asuntoPaisSeccionSubRepository)
        {
            _asuntoPaisSeccionSubRepository = asuntoPaisSeccionSubRepository;
        }
        public async Task<bool> Actualizar(AsuntoPaisSeccionSub modelo)
        {
            return await _asuntoPaisSeccionSubRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _asuntoPaisSeccionSubRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(AsuntoPaisSeccionSub modelo)
        {
            return await _asuntoPaisSeccionSubRepository.Insertar(modelo);
        }

        public async Task<AsuntoPaisSeccionSub> ObtenerPorId(int id)
        {
            return await _asuntoPaisSeccionSubRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<AsuntoPaisSeccionSub>> ObtenerTodos()
        {
            return await _asuntoPaisSeccionSubRepository.ObtenerTodos();
        }
    }
}
