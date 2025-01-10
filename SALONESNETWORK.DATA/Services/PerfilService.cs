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
    public class PerfilService:IPerfilService
    {
        private readonly IPerfilRepository<Perfil> _PerfilRepository;
        public PerfilService(IPerfilRepository<Perfil> PerfilRepository)
        {
            _PerfilRepository = PerfilRepository;
        }
        public async Task<bool> Actualizar(Perfil modelo)
        {
            return await _PerfilRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _PerfilRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Perfil modelo)
        {
            return await _PerfilRepository.Insertar(modelo);
        }

        public async Task<Perfil> ObtenerPorId(int id)
        {
            return await _PerfilRepository.ObtenerPorId(id);
        }

        public async Task<Perfil> ObtenerPorNombre(string nombreContacto)
        {
            IQueryable<Perfil> queryContactoSQL = await _PerfilRepository.ObtenerTodos();
            Perfil contacto = queryContactoSQL.Where(c => c.Nombre == nombreContacto).FirstOrDefault();
            return contacto;
        }

        public async Task<IQueryable<Perfil>> ObtenerTodos()
        {
            return await _PerfilRepository.ObtenerTodos();
        }
    }
}
