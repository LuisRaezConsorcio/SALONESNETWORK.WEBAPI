using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.DAL.Interfaces
{
    public interface IRegistroVisitaRepository<TEntityModel> where TEntityModel : class
    {
        Task<bool> Insertar(TEntityModel modelo);
        Task<bool> Eliminar(int id);
        Task<TEntityModel> ObtenerPorIdUsuario(int? Id_Usuario);
    }
}
