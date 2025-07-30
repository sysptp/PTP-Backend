
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso
{
    public class GnPermisoResponseForLoggin
    {
        public long PermisoId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool Query { get; set; }
    }
}
