<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/Usuario/UserResponse.cs
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Usuario
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Usuario/UserResponse.cs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? ScheduleId { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? UserImage { get; set; }
        public string? PersonalPhone { get; set; }
        public bool IsUserOnline { get; set; } = false;
        public long SucursalId { get; set; }
        public string? SucursalName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
