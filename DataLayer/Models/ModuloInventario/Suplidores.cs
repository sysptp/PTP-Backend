using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario
{
    [Table("Suplidores")]
    public class Suplidores
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public long? IdEmpresa { get; set; }

        [Required]
        public int? TipoIdentificacion { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NumeroIdentificacion { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Nombres { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Apellidos { get; set; }

        [Required]
        [MaxLength(20)]
        public string? TelefonoPrincipal { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DireccionPrincipal { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? PaginaWeb { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Descripcion { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        // Relación con Productos (muchos a muchos)
        public virtual ICollection<InvProductoSuplidor>? ProductoSuplidores { get; set; }
    }
}
