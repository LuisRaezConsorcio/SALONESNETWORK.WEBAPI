using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class AsuntoPaisSeccionSubDTO
    {
        public int Id { get; set; }
        public int? Id_Asunto { get; set; } // FK a Asunto

        public int? Id_Pais { get; set; } // FK a Pais

        public int? Id_Seccion { get; set; } // FK a Seccion

        public int? Id_SubSeccion { get; set; } // FK a SubSeccion
    }
}
