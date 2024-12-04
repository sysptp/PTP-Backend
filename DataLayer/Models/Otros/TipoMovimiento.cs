using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Otros
{
    [Table("InvTipoMovimiento")]
    public class TipoMovimiento
    {
        public int Id { get; set; }

        public long IdEmpresa { get; set; }

        public string? Nombre { get; set; }

        public bool? IN_OUT { get; set; }

        public bool Borrado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
