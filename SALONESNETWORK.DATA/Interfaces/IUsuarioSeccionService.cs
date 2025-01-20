using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IUsuarioSeccionService
    {
        Task<bool> Insertar(UsuarioSeccion modelo);
        Task<bool> Actualizar(UsuarioSeccion modelo);
        Task<bool> Eliminar(int id);
        Task<UsuarioSeccion> ObtenerPorId(int id);
        Task<IQueryable<UsuarioSeccion>> ObtenerTodos();
    }
}
