using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Configuracion.Seguridad.Permiso
{
    public class GnPermisoRequest
    {

        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public long CompanyId { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool Query { get; set; }
        [JsonIgnore]
        public long IDPermiso { get; set; }
    }
}
