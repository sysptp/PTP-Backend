using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloReporteria
{
    public class RepReportesConexiones
    {
        [Key]
        public int Id { get; set; } 

        [MaxLength(30)]
        public string? Servidor { get; set; } 

        [MaxLength(5)]
        public string? Puerto { get; set; } 

        [MaxLength(30)]
        public string? BaseDatos { get; set; } 

        [MaxLength(30)]
        public string? Usuario { get; set; } 

        [MaxLength(250)]
        public string? Clave { get; set; } 

        public bool? EsVerificable { get; set; } 

        public bool? Activo { get; set; } 

        public bool? Borrado { get; set; } 

        public DateTime? FechaAdicion { get; set; } 

        [MaxLength(50)]
        public string? UsuarioAdicion { get; set; } 

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; } 

        public DateTime? FechaModificacion { get; set; } 
    }
}
