using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.HelpDesk
{
    public class HdkSubCategory:AuditableEntities
    {
        [Key]
        public int IdSubCategory { get; set; }
        public int IdCategory { get; set; }
        public string Descripcion { get; set; }             
        public int CantidadHoraSLA { get; set; }
        public bool EsAsignacionDirecta { get; set; }
        public int IdDepartamento { get; set; }
        public int IdUsuarioAsignacion { get; set; }
        public long IdEmpresa { get; set; }
       
    }
}
