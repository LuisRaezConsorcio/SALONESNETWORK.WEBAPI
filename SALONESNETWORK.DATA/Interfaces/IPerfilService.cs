using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IPerfilService
    {
        Task<bool> Insertar(Perfil modelo);
        Task<bool> Actualizar(Perfil modelo);
        Task<bool> Eliminar(int id);
        Task<Perfil> ObtenerPorId(int id);
        Task<IQueryable<Perfil>> ObtenerTodos();
    }
}
