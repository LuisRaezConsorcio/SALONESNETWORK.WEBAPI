using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IAsuntoPaisSeccionSubService
    {
        Task<bool> Insertar(AsuntoPaisSeccionSub modelo);
        Task<bool> Actualizar(AsuntoPaisSeccionSub modelo);
        Task<bool> Eliminar(int id);
        Task<AsuntoPaisSeccionSub> ObtenerPorId(int id);
        Task<IQueryable<AsuntoPaisSeccionSub>> ObtenerTodos();
    }
}
