using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentGuest;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using DataLayer.Models.ModuloGeneral;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        [JsonIgnore]
        public int CompletedAppointments { get; set; } = 0;
        public int RepeatEvery { get; set; }
        public int RepeatUnitId { get; set; }
        [JsonPropertyName("AppointmentsInformation")]
        public AppointmentInformation AppointmentInformation { get; set; } = null!;
        [JsonPropertyName("Guests")]
        public List<CtaAppointmentGuestRequest>? CtaAppointvmentsRequest { get; set; }
    }
}
