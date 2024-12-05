using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails
{
    public class CtaSessionDetailsRequest 
    {
        [JsonIgnore]
        public int IdSessionDetail { get; set; }
        public int IdAppointment { get; set; }
        public int IdSession { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
