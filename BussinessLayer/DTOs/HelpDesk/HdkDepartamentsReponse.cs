using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkDepartamentsReponse:AuditableEntitiesReponse
    {
        public int IdDepartamentos { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
