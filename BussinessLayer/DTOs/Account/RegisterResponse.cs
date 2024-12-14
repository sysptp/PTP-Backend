<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Account/RegisterResponse.cs
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account
========
﻿namespace BussinessLayer.DTOs.Account
>>>>>>>> REFACTOR:BussinessLayer/DTOs/Account/RegisterResponse.cs
{
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? UserId { get; set; }
    }
}
