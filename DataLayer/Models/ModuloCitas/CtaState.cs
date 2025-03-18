using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaState : AuditableEntities
    {
        [Key]
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int EmailTemplateIdIn { get; set; }
        public int EmailTemplateIdOut { get; set; }
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; }
    }
}
