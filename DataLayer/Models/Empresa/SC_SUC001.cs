using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Empresa
{
    public class SC_SUC001
    {
        public long CODIGO_EMP { get; set; }
        [ForeignKey("CODIGO_EMP")]
        public GnEmpresa GnEmpresa { get; set; }

        [Key]
        public long CODIGO_SUC { get; set; }

        public string NOMBRE_SUC { get; set; }

        public string TELEFONO1 { get; set; }

        public int ID_USUARIO_RESPONSABLE { get; set; }
      
        public int Cod_Pais { get; set; }
      
        

        public int Cod_Region { get; set; }
       
        

        public int Cod_Provincia { get; set; }
       
      


        public int IdMunicipio { get; set; }
       
        

        public string DIRECCION { get; set; }

        public bool ESTADO_SUC { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }

        public string LONGITUD { get; set; }

        public string LATITUD { get; set; }

        public bool PRINCIPAL { get; set; }

    }
}
