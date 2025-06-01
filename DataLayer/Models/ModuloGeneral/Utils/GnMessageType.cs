using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Utils
{
    public class GnMessageType : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
