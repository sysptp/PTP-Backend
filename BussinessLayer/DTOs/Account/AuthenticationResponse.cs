using System.Text.Json.Serialization;

namespace BussinessLayer.Dtos.Account
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
        public bool IsVerified { get; set; }

        public int TokenDurationInMinutes { get; set; } 
        public DateTime RequestDate { get; set; } 
        public string IPUser { get; set; } = string.Empty;
        public long? CompanyId { get; set; }
        public long? SucursalId { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public string? Error{ get; set; }
        [JsonIgnore]
        public bool HasError { get; set; }
    }
}
