using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloInventario.Otros
{
    [Table("InvInventarioSucursal")]
    public class InvInventarioSucursal
    {
        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdEmpresa { get; set; }

        public int IdSucursal { get; set; }

        [MaxLength(500)]
        public string? UbicacionExhibicion { get; set; }

        [MaxLength(500)]
        public string? UbicacionGuardada { get; set; }

        public int? CantidadProducto { get; set; }

        public int? CantidadMinima { get; set; }

        public bool? Activo { get; set; }

        public bool? Borrado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(30)]
        public string? UsuarioCreacion { get; set; }

        [MaxLength(30)]
        public string? UsuarioModificacion { get; set; }

    }
}
