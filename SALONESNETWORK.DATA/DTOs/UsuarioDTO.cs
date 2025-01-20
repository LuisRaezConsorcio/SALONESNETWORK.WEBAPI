using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.DTOs
{
    public class UsuarioDTO
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
        //public string? Profiles { get; set; } //id o tabla intermedia con perfiles o perfilusuario
        public string? Token { get; set; }
    }
}
