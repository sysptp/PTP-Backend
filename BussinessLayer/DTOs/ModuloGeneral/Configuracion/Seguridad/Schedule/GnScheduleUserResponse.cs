namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule
{
    public class GnScheduleUserResponse
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string? UserName { get; set; }
        public string NameOfUser { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public GnScheduleResponse? Schedule { get; set; }
    }
}
