using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IRegistroVisitaService
    {
        Task<bool> Insertar(RegistroVisita modelo);
        Task<bool> Actualizar(RegistroVisita modelo);
        Task<bool> Eliminar(int id);
        Task<RegistroVisita> ObtenerPorId(int id);
        Task<IQueryable<RegistroVisita>> ObtenerTodos();
    }
}
