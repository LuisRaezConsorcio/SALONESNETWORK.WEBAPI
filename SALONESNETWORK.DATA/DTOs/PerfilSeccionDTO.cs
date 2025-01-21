using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class PerfilSeccionDTO
    {
        public int Id { get; set; }
        public int? Id_Perfil { get; set; } // FK a Perfil
        public int? Id_Seccion { get; set; } // FK a Seccion
        public Boolean? Estado { get; set; }
    }
}
