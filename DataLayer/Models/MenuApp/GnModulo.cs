using DataLayer.Models.Otros;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.MenuApp
{
  
    public class GnModulo : AuditableEntities
    {
        [Key]
        public int IDModulo { get; set; }

        public string Modulo { get; set; }

        public string ModuloIcon { get; set; }

        public int Orden { get; set; }

        public bool Borrado { get; set; }

        //Relación de 1 a muchos 
        public virtual ICollection<GnSubMenu> SubMenus { get; set; }
    }

}
