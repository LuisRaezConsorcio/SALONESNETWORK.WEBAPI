using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IDocumentoService
    {
        Task<bool> Insertar(Documento modelo);
        Task<bool> Actualizar(Documento modelo);
        Task<bool> Eliminar(int id);
        Task<Documento> ObtenerPorId(int id);
        Task<IQueryable<Documento>> ObtenerTodos();
    }
}
