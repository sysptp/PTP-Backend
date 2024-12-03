using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloGeneral.Monedas;

namespace BussinessLayer.DTOs.ModuloInventario.Impuestos
{
    public class ViewTaxDto
    {
        public int Id { get; set; }

        public int? IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal ValorImpuesto { get; set; }

        public string? NombreImpuesto { get; set; }

        public bool? Borrado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual Moneda? Moneda { get; set; }

        public virtual ICollection<InvProductoImpuesto>? ProductoImpuestos { get; set; }
    }
}
