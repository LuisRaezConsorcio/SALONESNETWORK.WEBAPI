using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IMensajeService
    {
        Task<bool> Insertar(Mensaje modelo);
        Task<bool> Actualizar(Mensaje modelo);
        Task<bool> Eliminar(int id);
        Task<Mensaje> ObtenerPorId(int id);
        Task<IQueryable<Mensaje>> ObtenerTodos();
    }
}
