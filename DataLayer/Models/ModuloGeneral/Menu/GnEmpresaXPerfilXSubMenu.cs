using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Menu
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
