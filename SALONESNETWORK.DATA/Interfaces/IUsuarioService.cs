using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> Insertar(Usuario modelo);
        Task<bool> Actualizar(Usuario modelo);
        Task<bool> Eliminar(int id);
        Task<Usuario> ObtenerPorId(int id);
        Task<IQueryable<Usuario>> ObtenerTodos();
    }
}
