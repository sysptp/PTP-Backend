using DataLayer.Models.Geografia;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    [Table("InvAlmacenes")]
    public class Almacenes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required]
        public int IdMunicipio { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        public long IdSucursal { get; set; }

        [Required]
        [MaxLength(60)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(1500)]
        public string? Direccion { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Required]
        public bool Borrado { get; set; }

        [Required]
        public bool EsPrincipal { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UsuarioCreacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; }

        // Navigation Property
        [ForeignKey("IdMunicipio")]
        public virtual Municipio? Municipio { get; set; }

    }
}
