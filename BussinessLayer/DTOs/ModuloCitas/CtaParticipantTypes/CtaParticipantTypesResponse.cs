
namespace BussinessLayer.DTOs.ModuloCitas.CtaParticipantTypes
{
    public class CtaParticipantTypesResponse
    {
        public int ParticipantTypeId { get; set; }
        public string ParticipantType { get; set; } = null!;
        public string? Description { get; set; }
    }
}
