using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository<Pais> _paisRepository;
        public PaisService(IPaisRepository<Pais> paisRepository)
        {
            _paisRepository = paisRepository;
        }
        public async Task<bool> Actualizar(Pais modelo)
        {
            return await _paisRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _paisRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Pais modelo)
        {
            return await _paisRepository.Insertar(modelo);
        }

        public async Task<Pais> ObtenerPorId(int id)
        {
            return await _paisRepository.ObtenerPorId(id);
        }

        public async Task<Pais> ObtenerPorNombre(string nombreContacto)
        {
            IQueryable<Pais> queryContactoSQL = await _paisRepository.ObtenerTodos();
            Pais contacto = queryContactoSQL.Where(c => c.Nombre == nombreContacto).FirstOrDefault();
            return contacto;
        }

        public async Task<IQueryable<Pais>> ObtenerTodos()
        {
            return await _paisRepository.ObtenerTodos();
        }
    }
}
