
namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkDepartamentsRequest
    {
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
