using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Seguridad
{
  public  class SC_IPSYS001
    {
        [Key]
        public int CODIGO_IPSYS { get; set; }

        public string IP { get; set; }

        public int CODIGO_USUARIO { get; set; }
        [ForeignKey("CODIGO_USUARIO")]
        public SC_USUAR001 SC_USUAR001 { get; set; }

        public int ID_USUARIO_ADICION { get; set; }

        public int IP_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int ID_USUARIO_MODIFICACION { get; set; }

        public int IP_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }

        public string LONGITUD { get; set; }

        public string LATITUD { get; set; }

    }
}
