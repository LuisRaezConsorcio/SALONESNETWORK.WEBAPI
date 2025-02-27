﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class Mensaje
    {
        public int Id { get; set; }
        public int? Id_TipoMensaje { get; set; }
        public int? Id_Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Boolean? Seguimiento { get; set; }
        public int? Id_MensajeSeguimiento { get; set; }
        public Boolean? Respuesta { get; set; }
        public int? Id_MensajeRespuesta { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public Boolean? Estado { get; set; }

        // Propiedad de navegación para la relación muchos a muchos
        public ICollection<DocumentoMensaje> DocumentoMensajes { get; set; }
        public ICollection<UbicacionMensaje> UbicacionMensajes { get; set; }
    }
}
