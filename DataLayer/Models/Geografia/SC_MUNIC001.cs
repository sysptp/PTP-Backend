using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Geografia
{
  public  class SC_MUNIC001
    {
        [Key]
        public int CODIGO_MUNIC { get; set; }

        public string NOMBRE_MUNIC { get; set; }

        public int CODIGO_PROV { get; set; }
        [ForeignKey("CODIGO_PROV")]
        public SC_PROV001 SC_PROV001 { get; set; }

        public int ID_USUARIO_ADICCION { get; set; }

        public int IP_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int ID_USUARIO_MODIFICACION { get; set; }

        public int IP_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }

        public string LONGITUD { get; set; }

        public string LATITUD { get; set; }

    }
}
