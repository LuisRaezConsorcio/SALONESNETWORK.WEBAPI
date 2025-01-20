using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IUbicacionMensajeService
    {
        Task<bool> Insertar(UbicacionMensaje modelo);
        Task<bool> Actualizar(UbicacionMensaje modelo);
        Task<bool> Eliminar(int id);
        Task<UbicacionMensaje> ObtenerPorId(int id);
        Task<IQueryable<UbicacionMensaje>> ObtenerTodos();
    }
}
