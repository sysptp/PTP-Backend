using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloVentas.Bancos
{
    public class TipoMovimientoBanco
    {
        [Key]
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string DebitoCreditootro { get; set; }
        public string InternoExterno { get; set; }
        public int IdUsuarioCrea { get; set; }
        [ForeignKey("IdUsuarioCrea")]
        public virtual SC_USUAR001 usuario { get; set; }
        public bool Estado { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual GnEmpresa empresa { get; set; }

    }
}
