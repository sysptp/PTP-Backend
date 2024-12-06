using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Monedas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ConciliacionTCTF
    {
        [Key]
        public long id { get; set; }

        public int idCaja { get; set; }

        [ForeignKey("idCaja")]
        public virtual Caja caja { get; set; }

        public int idFactura { get; set; }

        [ForeignKey("idFactura")]
        public virtual Facturacion facturacion { get; set; }

        public int idMoneda { get; set; }

        [ForeignKey("idMoneda")]
        public virtual Moneda moneda { get; set; }

        public string VTnoAuth { get; set; }

        public int VT4digit { get; set; }

        public string VTnoAuthEX { get; set; }

        public int VT4digitEX { get; set; }

        public string Conciliado { get; set; }

        public long IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa empresa { get; set; }

        public long IdSucursal { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual GnSucursal sucursal { get; set; }

    }

