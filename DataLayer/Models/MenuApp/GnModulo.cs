using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.MenuApp
{
  
    public class GnModulo : AuditableEntities
    {
        [Key]
        public int IDModulo { get; set; }

        public string Modulo { get; set; } = null!;
    }

}
