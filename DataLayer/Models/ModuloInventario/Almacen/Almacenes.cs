using DataLayer.Models.Geografia;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class Almacenes : BaseModel
    {
        [StringLength(60), Required]
        public string Nombre { get; set; }

        [StringLength(100), Required]
        public string Direccion { get; set; }

        [StringLength(20), Required]
        public string Telefono { get; set; }

        [Required]
        public int IdUSuarioACargo { get; set; }

        public long IDEmpresa { get; set; }

        public string AlmacenPrincipal { get; set; }

        public int IdMunicipio { get; set; }

        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; set; }

    }
}
