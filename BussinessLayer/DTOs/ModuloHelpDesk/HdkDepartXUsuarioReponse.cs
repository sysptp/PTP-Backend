using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkDepartXUsuarioReponse : AuditableEntitiesReponse
    {
        public int IdDepartXUsuario { get; set; }
        public string IdUsuarioDepto { get; set; }
        public int IdDepartamento { get; set; }
        public long IdEmpresa { get; set; }
    }
}
