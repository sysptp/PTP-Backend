using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Geografia
{
    public class Pais : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
