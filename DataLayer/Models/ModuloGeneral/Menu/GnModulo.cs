using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Menu
{

    public class GnModulo : AuditableEntities
    {
        [Key]
        public int IDModulo { get; set; }

        public string Modulo { get; set; } = null!;
    }

}
