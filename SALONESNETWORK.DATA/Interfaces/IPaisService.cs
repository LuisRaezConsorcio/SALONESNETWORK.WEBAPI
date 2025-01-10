using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IPaisService
    {
        Task<bool> Insertar(Pais modelo);
        Task<bool> Actualizar(Pais modelo);
        Task<bool> Eliminar(int id);
        Task<Pais> ObtenerPorId(int id);
        Task<IQueryable<Pais>> ObtenerTodos();

        Task<Pais> ObtenerPorNombre(string nombre);

    }
}
