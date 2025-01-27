using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkDepartXUsuarioReponse : AuditableEntitiesReponse
    {
        public int IdDepartXUsuario { get; set; }
        public string IdUsuarioDepto { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public int IdDepartamento { get; set; }
        public HdkDepartamentsReponse? HdkDepartamentsReponse { get; set; }
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
    }
}
