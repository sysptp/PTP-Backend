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
       
    }
}
