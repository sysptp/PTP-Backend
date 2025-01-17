using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements
{
    public class CtaAppointmentMovementsRequest 
    {
        [JsonIgnore]
        public int IdMovement { get; set; }
        public string? Description { get; set; }
        public int? EmailSmsType { get; set; }
        public string FromEmail { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public int? IdState { get; set; }
        public int? IdMessage { get; set; }
        public int AppointmentId { get; set; } 
        public bool Sent { get; set; } = true;
    }
}
