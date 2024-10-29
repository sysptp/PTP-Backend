using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Otros
{
    public class Tipo_Identificacion
    { 
        public int Cod_Empresa { get; set; }
        [Key]
        public int Cod_Tipo_Identificacion { get; set; }
        public string Descripcion { get; set; }
        public int Cod_Usuario { get; set; }
        public int Cod_Usuario_Modificacion { get; set; }
        public DateTime Fecha_adicion { get; set; }
        public DateTime Fecha_modificacion { get; set; }
        public bool Estado { get; set; }
    }
}
