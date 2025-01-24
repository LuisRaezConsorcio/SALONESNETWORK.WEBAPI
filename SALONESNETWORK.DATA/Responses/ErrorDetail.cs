using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class Error<T>
    {
        public string Mensaje { get; set; }
        public bool Resultado { get; set; }
        public T? Datos { get; set; }
    }

}
