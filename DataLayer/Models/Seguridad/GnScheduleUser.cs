
namespace DataLayer.Models.Seguridad
{
    public class GnScheduleUser
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime FechaAdicion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioAdicion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public bool Borrado { get; set; }

    }
}
