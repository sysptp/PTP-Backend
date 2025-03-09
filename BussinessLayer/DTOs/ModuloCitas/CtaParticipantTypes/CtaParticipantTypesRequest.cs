using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaParticipantTypes
{
    public class CtaParticipantTypesRequest
    {
        [JsonIgnore]
        public int ParticipantTypeId { get; set; }
        public string ParticipantType { get; set; } = null!;
        public string? Description { get; set; }
    }
}
