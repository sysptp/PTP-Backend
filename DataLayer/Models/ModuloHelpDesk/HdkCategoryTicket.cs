using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkCategoryTicket : AuditableEntities
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa GnEmpresa { get; set; }

    }
}
