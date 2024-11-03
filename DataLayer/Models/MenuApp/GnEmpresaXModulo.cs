using DataLayer.Models.Empresa;
using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    [Keyless]
    public class GnEmpresaXModulo : AuditableEntities
    {
        public long Codigo_EMP { get; set; }
        public int IDModulo { get; set; }
        public bool Borrado { get; set; }
        public virtual GnModulo Modulo { get; set; }
        public virtual GnEmpresa Empresa { get; set; }
    }

}
