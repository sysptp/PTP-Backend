using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral
{
    public class GnRepeatUnit : AuditableEntities
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

    }
}