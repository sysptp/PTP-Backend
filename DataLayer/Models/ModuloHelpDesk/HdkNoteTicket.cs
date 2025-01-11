using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkNoteTicket : AuditableEntities
    {
        [Key]
        public int IdNota { get; set; }
        public string Notas { get; set; } = null!;
        public int IdTicket { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
    }
}
