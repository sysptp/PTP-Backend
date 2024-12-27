using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Menu
{
    [Keyless]
    public class GnEmpresaXModulo : AuditableEntities
    {
        public long Codigo_EMP { get; set; }
        public int IDModulo { get; set; }
        public int IDMenu { get; set; }
        [ForeignKey("IDMenu")]
        public virtual GnMenu? Menu { get; set; }
        [ForeignKey("IDModulo")]
        public virtual GnModulo? Modulo { get; set; }
        [ForeignKey("Codigo_EMP")]
        public virtual GnEmpresa? Empresa { get; set; }
    }

}
