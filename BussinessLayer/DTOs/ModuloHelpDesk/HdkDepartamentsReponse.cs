using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkDepartamentsReponse : AuditableEntitiesReponse
    {
        public int IdDepartamentos { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
