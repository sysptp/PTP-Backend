<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/Permiso/GnPermisoResponse.cs
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Permiso
========
﻿namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Permiso/GnPermisoResponse.cs
{
    public class GnPermisoResponse
    {
        public long PermisoId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public long CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool Query { get; set; }
    }
}
