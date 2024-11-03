using DataLayer.Models.Empresa;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Seguridad
{
   public class SC_HORARIO001
   {
        public long CODIGO_EMP { get; set; }

        [ForeignKey("CODIGO_EMP")]
        public GnEmpresa GnEmpresa { get; set; }

        [Key]
        public int CODIGO_HRR { get; set; }

        public string DIA { get; set; }

        public bool BORRAR { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }
   }
}
