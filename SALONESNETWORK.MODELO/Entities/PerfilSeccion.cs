using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class PerfilSeccion
    {
        public int Id { get; set; }
        public int? Id_Perfil { get; set; } // FK a Perfil
        public Perfil Perfil { get; set; }

        public int? Id_Seccion { get; set; } // FK a Seccion
        public Seccion Seccion { get; set; }
        public bool Estado { get; set; }

    }
}
