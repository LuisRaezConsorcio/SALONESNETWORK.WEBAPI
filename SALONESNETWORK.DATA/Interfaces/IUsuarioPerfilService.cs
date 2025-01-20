using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IUsuarioPerfilService
    {
        Task<bool> Insertar(UsuarioPerfil modelo);
        //Task<bool> Actualizar(UsuarioPerfil modelo);
        Task<bool> Eliminar(int id);
        Task<bool> EliminarPorUsuario(int id);
        Task<bool> EliminarPorPerfil(int id);
        Task<int?> ObtenerId(UsuarioPerfil modelo);
        Task<IQueryable<UsuarioPerfil>> ObtenerTodos();
        Task<IQueryable<UsuarioPerfil>> ObtenerPorUsuario(UsuarioPerfil modelo);
        Task<IQueryable<UsuarioPerfil>> ObtenerPorPerfil(UsuarioPerfil modelo);

    }
}
