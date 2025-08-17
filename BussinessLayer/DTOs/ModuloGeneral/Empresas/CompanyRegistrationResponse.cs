using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;

namespace BussinessLayer.DTOs.ModuloGeneral.Empresas
{
    public class CompanyRegistrationResponse
    {
        public GnEmpresaResponse Company { get; set; } = null!;
        public UserResponse AdminUser { get; set; } = null!;
        public string SucursalName { get; set; } = null!;
        public long SucursalId { get; set; }
        public string ProfileName { get; set; } = null!;
        public int ProfileId { get; set; }
        public List<int> AssignedModuleIds { get; set; } = new List<int>();
        public string Message { get; set; } = null!;
    }
}