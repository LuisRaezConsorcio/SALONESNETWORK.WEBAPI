using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface ISeccionService
    {
        Task<bool> Insertar(Seccion modelo);
        Task<bool> Actualizar(Seccion modelo);
        Task<bool> Eliminar(int id);
        Task<Seccion> ObtenerPorId(int id);
        Task<IQueryable<Seccion>> ObtenerTodos();
    }
}
