namespace BussinessLayer.DTOs.ModuloCitas.CtaState
{
    public class CtaStateResponse 
    {
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public string? AreaDescription { get; set; }
        public bool IsDefault { get; set; } 
        public long TemplateIdIn { get; set; }
        public long TemplateIdOut { get; set; }
        public long TemplateIdUpdate { get; set; }
        public long TemplateIdUpdateParticipant { get; set; }
    }
}
