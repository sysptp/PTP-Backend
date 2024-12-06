using DataLayer.Models.ModuloGeneral.Empresa;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class SC_USUAR001
    {
        public long CODIGO_EMP { get; set; }
        [ForeignKey("CODIGO_EMP")]
        public GnEmpresa GnEmpresa { get; set; }

        [Key]
        public int CODIGO_USUARIO { get; set; }

        public string NOMBRE_USUARIO { get; set; }

        public string USUARIO { get; set; }

        public string PASSWOR { get; set; }

        public int ID_HORARIO { get; set; }


        public int ID_PERFIL { get; set; }


        public bool CORREO_CONFIRMADO { get; set; }

        public string IMAGEN_USUARIO { get; set; }

        public string CORREO { get; set; }

        public string TELEFONO_PERSONAL { get; set; }

        public string EXTENCION_PERSONAL { get; set; }

        public string TELEFONO { get; set; }

        public string EXTENCION { get; set; }

        public bool ONLINE_USUARIO { get; set; }

        public long CODIGO_SUC { get; set; }

        public string IP_ADICCION { get; set; }

        public string IP_MODIFICACION { get; set; }

        public int USUARIO_ADICCION { get; set; }

        public DateTime FECHA_ADICION { get; set; }

        public int USUARIO_MODIFICACION { get; set; }

        public DateTime FECHA_MODIFICACION { get; set; }

        public string LONGITUD { get; set; }

        public string LATITUD { get; set; }

    }
}
