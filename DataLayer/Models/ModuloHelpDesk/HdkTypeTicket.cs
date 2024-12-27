using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkTypeTicket : AuditableEntities
    {
        [Key]
        public int IdTipoTicket { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa? GnEmpresa { get; set; }
    }
}
