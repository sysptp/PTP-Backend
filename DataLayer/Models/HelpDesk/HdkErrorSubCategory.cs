using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.HelpDesk
{
    public class HdkErrorSubCategory: AuditableEntities
    {
        [Key]
        public int IdErroSubCategory { get; set; }
        public int IdSubCategory { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
       
    }
}
