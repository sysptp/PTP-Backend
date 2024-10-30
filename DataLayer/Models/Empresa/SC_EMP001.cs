using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Empresa
{
    public class SC_EMP001
    {
        [Key]
        public long CODIGO_EMP { get; set; }

        [Required(ErrorMessage = "Campo Nombre Empresa Requerido")]
        public string? NOMBRE_EMP { get; set; }

        public string? LOGO_EMP { get; set; }

        public string? RNC_EMP { get; set; }

        [Required(ErrorMessage = "Campo Direccion Requerido")]
        public string? DIRECCION { get; set; }

        [Required(ErrorMessage = "Campo Telefono Principal Requerido")]
        public string? TELEFONO1 { get; set; }

        public string? TELEFONO2 { get; set; }

        public string? EXT_TEL1 { get; set; }

        public string? EXT_TEL2 { get; set; }

        [Required(ErrorMessage = "Campo Cantidad Sucursales  Requerido")]
        public int CANT_SUCURSALES { get; set; }
        [Required(ErrorMessage = "Campo Cantidad Usuario Requerido")]
        public int CANT_USUARIO { get; set; }
        public string? WEB { get; set; }
        public int USUARIO_ADICCION { get; set; }
        public DateTime FECHA_ADICION { get; set; }
        public int USUARIO_MODIFICACION { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
    }
}
