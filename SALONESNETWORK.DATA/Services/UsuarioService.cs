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
    public class UsuarioService:IUsuarioService
    {
        private readonly IUsuarioRepository<Usuario> _usuarioRepository;
        public UsuarioService(IUsuarioRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<bool> Actualizar(Usuario modelo)
        {
            return await _usuarioRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _usuarioRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Usuario modelo)
        {
            return await _usuarioRepository.Insertar(modelo);
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await _usuarioRepository.ObtenerPorId(id);
        }

        public async Task<IQueryable<Usuario>> ObtenerTodos()
        {
            return await _usuarioRepository.ObtenerTodos();
        }
    }
}
