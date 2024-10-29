using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Geografia
{
    public class Ciudades_X_Paises
    {
        public int Cod_Empresa { get; set; }
        [Key]
        public int Cod_ciudades { get; set; }
        public int Cod_pais { get; set; }
        public string Descripcion { get; set; }
        public int Cod_Usuario { get; set; }
        public int Cod_Usuario_Modificacion { get; set; }
        public DateTime Fecha_adicion { get; set; }
        public DateTime Fecha_modificacion { get; set; }
        public bool Estado { get; set; }

    }
}
