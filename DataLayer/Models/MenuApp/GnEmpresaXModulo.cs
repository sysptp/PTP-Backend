using DataLayer.Models.Empresa;
using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    public class GnEmpresaXModulo : AuditableEntities
    {
        [Key, Column(Order = 0)]
        public long Codigo_EMP { get; set; }  

        [Key, Column(Order = 1)]
        public int IDModulo { get; set; }
        public bool Borrado { get; set; }
        public virtual GnModulo Modulo { get; set; }
        public virtual SC_EMP001 Empresa { get; set; }
    }

}
