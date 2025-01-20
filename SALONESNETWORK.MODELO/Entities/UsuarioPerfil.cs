using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class UsuarioPerfil
    {
        public int Id { get; set; }
        public int? Id_Usuario { get; set; } // FK a Usuario
        public Usuario Usuario { get; set; }

        public int? Id_Perfil { get; set; } // FK a Perfil
        public Perfil Perfil { get; set; }
        public bool Estado { get; set; }

    }
}
