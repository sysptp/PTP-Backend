using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.MenuApp
{
    public class GnMenu
    {
        [Key]
        public int IDMenu { get; set; }
        public string Menu { get; set; }
        public int Nivel { get; set; }
        public int Orden { get; set; }
        public string URL { get; set; }
        [NotMapped]
        public bool Check { get; set; }
        public string MenuIcon { get; set; }
        public int IdModulo { get; set; }
        public int menupadre { get; set; }
    }
}
