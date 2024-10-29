using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Geografia
{
    public class Region:BaseModel
    {
        public string Nombre { get; set; }

        public int IdPais { get; set; }
        [ForeignKey("IdPais")]
        public virtual Pais Pais { get; set; }

    }
}
