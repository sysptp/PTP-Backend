
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaMeetingPlace : AuditableEntities
    {
        [Key]
        public int IdMeetingPlace { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
       
    }
}
