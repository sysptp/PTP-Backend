using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Geografia
{
    public class SC_PAIS001
    {
        [Key]
        public int CODIGO_PAIS { get; set; }

        public string NOMBRE_PAIS { get; set; }

        public string COD_ISO_2 { get; set; }

        public string COD_ISO_3 { get; set; }

        public int COD_ISO_NUMERICO { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }

        public string LONGITUD { get; set; }

        public string LATITUD { get; set; }

        public string Nacionalidad { get; set; }
    }
}
