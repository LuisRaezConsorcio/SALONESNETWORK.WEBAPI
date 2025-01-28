using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.MODELS.Entities
{
    public class Perfil
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public Boolean? Estado { get; set; }

        // Propiedad de navegación para la relación muchos a muchos
        public ICollection<UsuarioPerfil> UsuarioPerfiles { get; set; }
        public ICollection<PerfilSeccion> PerfilSecciones { get; set; }
    }
}
