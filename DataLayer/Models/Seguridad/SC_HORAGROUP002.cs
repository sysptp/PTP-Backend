using DataLayer.Models.Empresa;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Seguridad
{
    public class SC_HORAGROUP002
    {

        public long CODIGO_EMP { get; set; }

        [ForeignKey("CODIGO_EMP")]
        public GnEmpresa GnEmpresa { get; set; }

        [Key]
        public int CODIGO_HRRGROUP { get; set; }

        public int CODIGO_HRR_1 { get; set; }

        public int CODIGO_HRR_2 { get; set; }

        public int CODIGO_HRR_3 { get; set; }

        public int CODIGO_HRR_4 { get; set; }

        public int CODIGO_HRR_5 { get; set; }

        public int CODIGO_HRR_6 { get; set; }

        public int CODIGO_HRR_7 { get; set; }

        public bool BORRAR { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }
    }
}
