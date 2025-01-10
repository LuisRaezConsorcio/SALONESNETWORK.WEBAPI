using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class DocumentoMensaje
    {
        public int Id { get; set; }
        public int? Id_Mensaje { get; set; } // FK a Mensaje
        public Mensaje Mensaje { get; set; }

        public int? Id_Documento { get; set; } // FK a Documento
        public Documento Documento { get; set; }
    }
}
