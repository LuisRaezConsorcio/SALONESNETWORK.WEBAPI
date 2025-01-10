using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class DocumentoMensajeDTO
    {
        public int Id { get; set; }
        public int? Id_Mensaje { get; set; } // FK a Mensaje
        public MensajeDTO Mensaje { get; set; }

        public int? Id_Documento { get; set; } // FK a Documento
        public DocumentoDTO Documento { get; set; }
    }
}
