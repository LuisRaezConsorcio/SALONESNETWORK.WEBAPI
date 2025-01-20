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
    public class UsuarioSeccionService:IUsuarioSeccionService
    {
        private readonly IUsuarioSeccionRepository<UsuarioSeccion> _usuarioSeccionRepository;
        public UsuarioSeccionService(IUsuarioSeccionRepository<UsuarioSeccion> usuarioSeccionRepository)
        {
            _usuarioSeccionRepository = usuarioSeccionRepository;
        }
        public async Task<bool> Actualizar(UsuarioSeccion modelo)
        {
            return await _usuarioSeccionRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _usuarioSeccionRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(UsuarioSeccion modelo)
        {
            return await _usuarioSeccionRepository.Insertar(modelo);
        }

        public async Task<UsuarioSeccion> ObtenerPorId(int id)
        {
            return await _usuarioSeccionRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<UsuarioSeccion>> ObtenerTodos()
        {
            return await _usuarioSeccionRepository.ObtenerTodos();
        }
    }
}
