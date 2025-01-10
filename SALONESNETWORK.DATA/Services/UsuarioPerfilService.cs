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
    public class UsuarioPerfilService:IUsuarioPerfilService
    {
        private readonly IUsuarioPerfilRepository<UsuarioPerfil> _usuarioPerfilRepository;
        public UsuarioPerfilService(IUsuarioPerfilRepository<UsuarioPerfil> usuarioPerfilRepository)
        {
            _usuarioPerfilRepository = usuarioPerfilRepository;
        }
        public async Task<bool> Actualizar(UsuarioPerfil modelo)
        {
            return await _usuarioPerfilRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _usuarioPerfilRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(UsuarioPerfil modelo)
        {
            return await _usuarioPerfilRepository.Insertar(modelo);
        }

        public async Task<UsuarioPerfil> ObtenerPorId(int id)
        {
            return await _usuarioPerfilRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<UsuarioPerfil>> ObtenerTodos()
        {
            return await _usuarioPerfilRepository.ObtenerTodos();
        }
    }
}
