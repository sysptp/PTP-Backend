using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Account/AuthenticationResponse.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account
========
namespace BussinessLayer.DTOs.Account
>>>>>>>> REFACTOR:BussinessLayer/DTOs/Account/AuthenticationResponse.cs
{
    public class AuthenticationResponse
    {
        public string? JWToken { get; set; }
        public long? Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = null!;
        public int? RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsVerified { get; set; }

        public int TokenDurationInMinutes { get; set; }
        public DateTime RequestDate { get; set; }
        public string IPUser { get; set; } = string.Empty;
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public long? SucursalId { get; set; }
        public string? SucursalName { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public string? Error { get; set; }
        [JsonIgnore]
        public bool HasError { get; set; }
    }
}
