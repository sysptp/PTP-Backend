using DataLayer.Models.ModuloInventario.Productos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Imagen
{
    [Table("GnImagenes")]
    public class Imagen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        [Required]
        public string? Url { get; set; }

        [Required]
        public bool? EsPrincipal { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        public virtual ICollection<InvProductoImagen>? InvProductoImagenes { get; set; }
    }
}
