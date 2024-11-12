using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.HelpDesk
{
    public class HdkCategoryTicket:AuditableEntities
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
       
    }
}
