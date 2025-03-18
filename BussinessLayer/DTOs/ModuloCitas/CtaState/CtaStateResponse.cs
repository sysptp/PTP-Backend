namespace BussinessLayer.DTOs.ModuloCitas.CtaState
{
    public class CtaStateResponse 
    {
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long EmailTemplateIdIn { get; set; }
        public long EmailTemplateIdOut { get; set; }
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; }


    }
}
