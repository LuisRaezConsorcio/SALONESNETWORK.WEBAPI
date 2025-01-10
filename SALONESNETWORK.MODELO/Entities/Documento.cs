using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }

        // Propiedad de navegación para la relación muchos a muchos
        public ICollection<DocumentoMensaje> DocumentoMensajes { get; set; }

    }
}
