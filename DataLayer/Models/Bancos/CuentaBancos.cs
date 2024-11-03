using DataLayer.Models.Caja;
using DataLayer.Models.Empresa;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Bancos
{
    public class CuentaBancos
    {
        [Key]
        public int Id { get; set; }

        public int IdBanco { get; set; }

        [ForeignKey("IdBanco")]
        public virtual Bancos banco { get; set; }

        public int IdMoneda { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda moneda { get; set; }

        public string NombreCuenta { get; set; }

        public string NumeroCuenta { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int idUsuarioCrea { get; set; }

        public DateTime FechaModificacion { get; set; }

        public bool Estado { get; set; }

        public long IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa empresa { get; set; }

        public long IdSucursal { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual GnSucursal sucursal { get; set; }
    }
}
