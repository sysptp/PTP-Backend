using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Marcas
{
    [Table("InvMarcas")]
    public class InvMarcas
    {
        [Key]
        public int? Id { get; set; }

        public long? IdEmpresa { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Nombre { get; set; }

        [Required]
        public bool? Activo { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        // Relación con Versiones (uno a muchos)
        public virtual ICollection<Versiones>? Versiones { get; set; }

    }
}
