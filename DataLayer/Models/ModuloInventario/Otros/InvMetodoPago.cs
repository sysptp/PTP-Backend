using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Otros
{
    [Table("InvMetodoPago")]
    public class InvMetodoPago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Nombre { get; set; }

        [Required]
        public bool EsEfectivo { get; set; }

        [Required]
        public bool Borrado { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; }

        public bool? Activo { get; set; }
    }
}
