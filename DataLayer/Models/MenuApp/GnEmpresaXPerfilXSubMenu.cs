using DataLayer.Models.Empresa;
using DataLayer.Models.Otros;
using DataLayer.Models.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    public class GnEmpresaXPerfilXSubMenu : AuditableEntities
    {
        [Key, Column(Order = 0)]
        public int IDPerfil { get; set; }

        [Key, Column(Order = 1)]
        public int IDSubMenu { get; set; }

        [Key, Column(Order = 2)]
        public long Codigo_EMP { get; set; }
        public bool Borrado { get; set; }
        public virtual Gn_Perfil Perfil { get; set; }
        public virtual GnSubMenu SubMenu { get; set; }
        public virtual SC_EMP001 Empresa { get; set; }
    }

}
