using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class UsuarioPerfilDTO
    {
        public int Id { get; set; }
        public int? Id_Usuario { get; set; } // FK a Usuario
        public UsuarioDTO Usuario { get; set; }

        public int? Id_Perfil { get; set; } // FK a Perfil
        public PerfilDTO Perfil { get; set; }
    }
}
