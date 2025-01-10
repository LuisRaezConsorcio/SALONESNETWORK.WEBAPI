using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface ITipoMensajeService
    {
        Task<bool> Insertar(TipoMensaje modelo);
        Task<bool> Actualizar(TipoMensaje modelo);
        Task<bool> Eliminar(int id);
        Task<TipoMensaje> ObtenerPorId(int id);
        Task<IQueryable<TipoMensaje>> ObtenerTodos();
    }
}
