using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class UsuarioSeccion
    {
        public int Id { get; set; }
        public int? Id_Usuario { get; set; } // FK a Usuario
        public Usuario Usuario { get; set; }

        public int? Id_Seccion { get; set; } // FK a Seccion
        public Seccion Seccion { get; set; }

        public Boolean? Estado { get; set; }
    }
}
