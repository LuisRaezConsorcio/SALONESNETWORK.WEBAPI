using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public int? Name { get; set; }

        // Propiedad de navegación para la relación muchos a muchos
        public ICollection<UsuarioPerfil> UsuarioPerfiles { get; set; }
    }
}
