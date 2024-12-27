using DataLayer.Models.ModuloGeneral.Empresa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class SC_HORA_X_USR002
    {

        public long CODIGO_EMP { get; set; }

        [ForeignKey("CODIGO_EMP")]
        public GnEmpresa GnEmpresa { get; set; }

        public int CODIGO_USUARIO { get; set; }

        [Key]
        public int CODIGO_HRR { get; set; }

        public int CODIGO_HRRUSR { get; set; }

        public int MINUTOS_PRORROGA { get; set; }

        public bool BORRAR { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }
    }
}
