using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IDocumentoMensajeService
    {
        Task<bool> Insertar(DocumentoMensaje modelo);
        Task<bool> Actualizar(DocumentoMensaje modelo);
        Task<bool> Eliminar(int id);
        Task<DocumentoMensaje> ObtenerPorId(int id);
        Task<IQueryable<DocumentoMensaje>> ObtenerTodos();
    }
}
