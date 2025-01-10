using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement
{
    public class CtaAppointmentManagementRequest 
    {
        [JsonIgnore]
        public int IdManagementAppointment { get; set; }
        public int IdAppointment { get; set; }
        public string? Comment { get; set; }
    }
}
