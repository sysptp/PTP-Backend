using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    public class GnMenu : AuditableEntities
    {
        [Key]
        public int IDMenu { get; set; }
        public string Menu { get; set; } = null!;
        public int Nivel { get; set; }
        public int Orden { get; set; }
        public string? URL { get; set; } 
        public string? MenuIcon { get; set; }
        public int IdModulo { get; set; }
        public int MenuPadre { get; set; }
    }
}
