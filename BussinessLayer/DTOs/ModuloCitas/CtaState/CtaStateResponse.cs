namespace BussinessLayer.DTOs.ModuloCitas.CtaState
{
    public class CtaStateResponse 
    {
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
