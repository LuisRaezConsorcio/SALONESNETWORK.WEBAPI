using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class UsuarioSeccionDTO
    {
        public int Id { get; set; }
        public int? Id_Usuario { get; set; } // FK a Usuario
        public int? Id_Seccion { get; set; } // FK a Seccion
        public Boolean? Estado { get; set; }
    }
}
