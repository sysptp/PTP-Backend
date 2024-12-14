<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/Autenticacion/LoginRequestDTO.cs
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Autenticacion
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Autenticacion/LoginRequestDTO.cs
{
    public class LoginRequestDTO
    {
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
