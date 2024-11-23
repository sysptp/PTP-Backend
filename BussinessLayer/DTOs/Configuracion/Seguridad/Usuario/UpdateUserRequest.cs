
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Configuracion.Seguridad.Usuario
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public int RoleId { get; set; }
        public long CompanyId { get; set; }
        public long SucursalId { get; set; }
        public int ScheduleId { get; set; }
        public string? UserIP { get; set; }
        public bool IsActive { get; set; }
        public string? UserImage {  get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}
