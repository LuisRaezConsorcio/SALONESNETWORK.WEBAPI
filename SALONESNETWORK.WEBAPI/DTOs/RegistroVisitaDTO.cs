using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.WEBAPI.DTOs
{
    public class RegistroVisitaDTO
    {
        public int Id { get; set; }
        public int? Id_Usuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Ip { get; set; }
    }
}
