<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DMunicipio/MunicipioResponse.cs
﻿using DataLayer.Models.Geografia;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DMunicipio
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DMunicipio/MunicipioResponse.cs
{
    public class MunicipioResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
