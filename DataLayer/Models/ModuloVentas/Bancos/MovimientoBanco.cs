using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloVentas.Bancos
{
    public class MovimientoBanco
    {
        [Key]
        public long Id { get; set; }
        public int IdBanco { get; set; }
        public DateTime Fecha_movimiento { get; set; }
        public string Detalle { get; set; }
        public int idTipoMovimientoBanco { get; set; }
        public int idUsuario { get; set; }
        [ForeignKey("idUsuario")]
        public virtual SC_USUAR001 usuario { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa empresa { get; set; }
        public long IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public virtual GnSucursal sucursal { get; set; }
        public int IdMoneda { get; set; }

    }
}
