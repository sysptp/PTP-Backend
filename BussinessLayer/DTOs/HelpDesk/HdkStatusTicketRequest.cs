
namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkStatusTicketRequest
    {
        public string Descripcion { get; set; }
        public bool EsCierre { get; set; }
        public long IdEmpresa { get; set; }
    }
}
