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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public int? EmployedId { get; set; }
        public int? LocalTypeId { get; set; }
        public int? LocalId { get; set; }
        public int? AreaId { get; set; }
        public int? UserLocalId { get; set; }
        public int? UserLocalId2 { get; set; }
        public int? UserLocalId3 { get; set; }
        public int? UserLocalComercialId { get; set; }
        public string? Token { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public Boolean? Estado { get; set; }

        // Propiedad de navegación para la relación muchos a muchos
        public ICollection<UsuarioPerfil> UsuarioPerfiles { get; set; }
        public ICollection<UsuarioSeccion> UsuarioSecciones { get; set; }
    }
}
