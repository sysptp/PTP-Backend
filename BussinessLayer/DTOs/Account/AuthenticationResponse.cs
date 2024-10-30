﻿using System.Text.Json.Serialization;

namespace BussinessLayer.Dtos.Account
{
    public class AuthenticationResponse
    {
        public long? Id { get; set; } 
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string>? Roles { get; set; } 
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? JWToken { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
