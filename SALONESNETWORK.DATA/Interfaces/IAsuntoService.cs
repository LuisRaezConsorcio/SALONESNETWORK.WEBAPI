using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IAsuntoService
    {
        Task<bool> Insertar(Asunto modelo);
        Task<bool> Actualizar(Asunto modelo);
        Task<bool> Eliminar(int id);
        Task<Asunto> ObtenerPorId(int id);
        Task<IQueryable<Asunto>> ObtenerTodos();
    }
}
