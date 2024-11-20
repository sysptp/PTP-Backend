using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.HelpDesk
{
    public class HdkStatusTicket:AuditableEntities
    {
        [Key]
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public bool EsCierre { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; } 
        public int OrdenStatus { get; set; }

    }
}
