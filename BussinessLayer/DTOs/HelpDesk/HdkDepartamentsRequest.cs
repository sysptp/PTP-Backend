
namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkDepartamentsRequest
    {
        public string Decripcion { get; set; }
        public long IdEmpresa { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
