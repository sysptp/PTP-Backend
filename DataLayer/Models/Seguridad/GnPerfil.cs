using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Entities
{
    public class GnPerfil 
    {
        [Key]
        public int IDPerfil { get; set; }
        public string Perfil { get; set; }
        public string? Descripcion { get; set; }
        public int? Bloquear { get; set; }
        public long? IDEmpresa { get; set; }
        public DateTime FechaCreada { get; set; } = DateTime.Now;
        public DateTime? UltimaFechaModificacion { get; set; }
    }
}
