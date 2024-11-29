
namespace DataLayer.Models.Seguridad
{
    public class GnSchedule
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public decimal StartHour { get; set; }
        public decimal EndHour { get; set; }
        public string StartDay { get; set; } = null!;
        public string EndDay { get; set; } = null!;
        public DateTime FechaAdicion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioAdicion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public bool Borrado { get; set; }

    }
}
