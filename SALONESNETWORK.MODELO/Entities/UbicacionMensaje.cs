﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class UbicacionMensaje
    {
        public int Id { get; set; }

        public int? Id_Mensaje { get; set; } // FK a Mensaje
        public Mensaje? Mensaje { get; set; }


        public int? Id_Asunto { get; set; } // FK a Asunto
        public Asunto? Asunto { get; set; }

        public int? Id_Pais { get; set; } // FK a Pais
        public Pais? Pais { get; set; }

        public int? Id_Seccion { get; set; } // FK a Seccion
        public Seccion? Seccion { get; set; }

        public int? Id_SubSeccion { get; set; } // FK a SubSeccion
        public SubSeccion? SubSeccion { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public Boolean? Estado { get; set; }

    }
}
