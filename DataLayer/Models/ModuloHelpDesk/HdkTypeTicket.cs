using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkTypeTicket : AuditableEntities
    {
        [Key]
        public int IdTipoTicket { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }

    }
}
