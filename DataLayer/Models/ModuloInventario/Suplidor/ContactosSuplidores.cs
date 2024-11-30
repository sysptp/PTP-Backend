using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("InvContactosSuplidores")]
    public class ContactosSuplidores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdSuplidor { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Telefono1 { get; set; }

        [MaxLength(30)]
        public string? Telefono2 { get; set; }

        [MaxLength(30)]
        public string? Extension { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public bool Borrado { get; set; }

        public long? IdEmpresa { get; set; }

        // Navigation property for the relationship with InvSuplidores
        [ForeignKey("IdSuplidor")]
        public virtual Suplidores? Suplidor { get; set; }
    }

