using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkDepartXUsuarioReponse: AuditableEntitiesReponse
    {
        public int IdDepartXUsuario { get; set; }
        public int IdDepartamento { get; set; }
        public long IdEmpresa { get; set; }
    }
}
