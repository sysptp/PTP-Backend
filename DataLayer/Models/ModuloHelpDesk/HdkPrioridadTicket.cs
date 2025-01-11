using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkPrioridadTicket : AuditableEntities
    {
        [Key]
        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
    }
}
