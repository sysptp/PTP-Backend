<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DPais/CountryResponse.cs
﻿using BussinessLayer.Atributes;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DPais/CountryResponse.cs
{

    [TableName("Pais")]
    public class CountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
