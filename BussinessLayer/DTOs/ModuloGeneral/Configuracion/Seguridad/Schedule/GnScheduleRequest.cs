using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule
{
    public class GnScheduleRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public decimal StartHour { get; set; }
        public decimal EndHour { get; set; }
        public string StartDay { get; set; } = null!;
        public string EndDay { get; set; } = null!;
    }
}
