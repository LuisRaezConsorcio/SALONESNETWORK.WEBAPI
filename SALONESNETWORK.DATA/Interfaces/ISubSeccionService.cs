using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface ISubSeccionService
    {
        Task<bool> Insertar(SubSeccion modelo);
        Task<bool> Actualizar(SubSeccion modelo);
        Task<bool> Eliminar(int id);
        Task<SubSeccion> ObtenerPorId(int id);
        Task<IQueryable<SubSeccion>> ObtenerTodos();
    }
}
