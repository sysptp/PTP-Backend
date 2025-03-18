using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaState
{
    public class CtaStateRequest 
    {
        [JsonIgnore]
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int EmailTemplateIdIn { get; set; }
        public int EmailTemplateIdOut { get; set; }
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; }


    }
}
