<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Menu/GnMenuResponse.cs
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Menu
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Menu
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Menu/GnMenuResponse.cs
{
    public class GnMenuResponse
    {
        public int MenuID { get; set; }
        public string Name { get; set; } = null!;
        public int Level { get; set; }
        public int Order { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int ModuleID { get; set; }
        public int ParentMenuId { get; set; }
        public bool Query { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public List<GnMenuResponse>? SubMenus { get; set; } = new List<GnMenuResponse>();
    }

}