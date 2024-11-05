using DataLayer.Models.ModuloInventario;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Suplidor
{
    public class ContactosSuplidores : BaseModel
    {
        public int IdSuplidor { get; set; }

        [ForeignKey("IdSuplidor")]
        public virtual Suplidores Suplidores { get; set; }

        [StringLength(30),Required]
        public string Nombre { get; set; }

        [StringLength(30), Required]
        public string Telefono1 { get; set; }

        [StringLength(30)]
        public string Telefono2 { get; set; }

        [StringLength(30)]
        public string Extension { get; set; }
    }
}
