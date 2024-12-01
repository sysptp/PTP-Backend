namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule
{
    public class GnScheduleUserRequest
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
    }
}
