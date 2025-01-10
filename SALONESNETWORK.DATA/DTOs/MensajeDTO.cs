using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class MensajeDTO
    {
        public int Id { get; set; }
        public int? Id_TipoMensaje { get; set; }
        public int? Id_Usuario { get; set; }
        public int? Id_Asunto { get; set; }
        public int? Id_Pais { get; set; }
        public int? Id_Seccion { get; set; }
        public int? Id_SubSeccion { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public Boolean? Seguimiento { get; set; }
        public int? Id_MensajeSeguimiento { get; set; }
        public Boolean? Respuesta { get; set; }
        public int? Id_MensajeRespuesta { get; set; }
    }
}
