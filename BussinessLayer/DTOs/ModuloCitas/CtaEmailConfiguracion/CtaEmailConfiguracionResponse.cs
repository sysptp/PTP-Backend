
namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion
{
    public class CtaEmailConfiguracionResponse
    {
        public int IdEmailConfiguration { get; set; }
        public int IdUser { get; set; }
        public string? NameOfUser { get; set; }
        public bool IsMailbox { get; set; } = false;
        public string Email { get; set; } = null!;
    }
}
