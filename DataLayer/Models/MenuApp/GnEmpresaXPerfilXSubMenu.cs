using DataLayer.Models.Empresa;
using DataLayer.Models.Otros;
using DataLayer.Models.Seguridad;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    [Keyless]
    public class GnEmpresaXPerfilXSubMenu : AuditableEntities
    {
        public int IDPerfil { get; set; }
        public int IDSubMenu { get; set; }
        public long Codigo_EMP { get; set; }
        public bool Borrado { get; set; }
        public virtual Gn_Perfil Perfil { get; set; }
        public virtual GnSubMenu SubMenu { get; set; }
        public virtual GnEmpresa Empresa { get; set; }
    }
}
