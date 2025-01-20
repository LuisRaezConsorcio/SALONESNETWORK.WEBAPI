using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.DAL.Interfaces
{
    public interface IUsuarioPerfilRepository<TEntityModel> where TEntityModel : class
    {
        Task<bool> Insertar(TEntityModel modelo);
        //Task<bool> Actualizar(TEntityModel modelo);
        Task<bool> Eliminar(int id); 
        Task<bool> EliminarPorUsuario(int id); 
        Task<bool> EliminarPorPerfil(int id);
        Task<int?> ObtenerId(TEntityModel modelo);
        Task<IQueryable<TEntityModel>> ObtenerTodos();
        Task<IQueryable<TEntityModel>> ObtenerPorUsuario(TEntityModel modelo);
        Task<IQueryable<TEntityModel>> ObtenerPorPerfil(TEntityModel modelo);
    }
}
