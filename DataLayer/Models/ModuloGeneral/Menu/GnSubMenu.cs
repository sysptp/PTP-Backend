using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Menu
{
    public class GnSubMenu : AuditableEntities
    {
        [Key]
        public int IDSubMenu { get; set; }
        public string SubMenu { get; set; }
        public string URL { get; set; }
        public string MenuIcon { get; set; }
        public int Orden { get; set; }
        public bool Borrado { get; set; }
        public int IDModulo { get; set; }
        public virtual GnModulo Modulo { get; set; }
    }

}
