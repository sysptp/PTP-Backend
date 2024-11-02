using DataLayer.Models.Empresa;
using DataLayer.Models.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Caja
{
    public class AperturaCierreCaja
    {
        [Key]
        public long id { get; set; }

        public DateTime Fecha_apertura { get; set; }

        public string AperturCierre { get; set; }

        public int idCaja { get; set; }

        [ForeignKey("idCaja")]
        public virtual Caja caja { get; set; }

        public string DeclaroFatante { get; set; }

        public int IdMoneda { get; set; }

        public int idUsuarioReponCaja { get; set; }

        [ForeignKey("idUsuarioReponCaja")]
        public virtual SC_USUAR001 usuarioRespCaja { get; set; }

        public int totalOperacionesEfectivo { get; set; }

        public int totalOperacionesTajeta { get; set; }

        public string ConciliadoAlCuadre { get; set; }

        public int idUsuarioConfirmaCO { get; set; }

        [ForeignKey("idUsuarioConfirmaCO")]
        public virtual SC_USUAR001 usuarioConfirmaCierreA { get; set; }

        public string ConciliarTJoTransaferencia { get; set; }

        public int idUsuarioConfirmaTjoTF { get; set; }

        [ForeignKey("idUsuarioConfirmaTjoTF")]
        public virtual SC_USUAR001 usuarioConfirmaTF { get; set; }

        public int idConciliacion { get; set; }

        public DateTime FechaCoinciliacionTJoTF { get; set; }

        public long IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa empresa { get; set; }

        public long IdSucursal { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual GnSucursal sucursal { get; set; }
    }
}
