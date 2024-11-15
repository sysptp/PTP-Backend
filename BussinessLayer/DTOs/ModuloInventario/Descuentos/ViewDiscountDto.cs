using DataLayer.Models.ModuloInventario.Productos;

namespace BussinessLayer.DTOs.ModuloInventario.Descuentos
{
    public class ViewDiscountDto
    {
        public int Id { get; set; }

        public int? IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal? ValorDescuento { get; set; }

        public bool? EsPermanente { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool? Activo { get; set; }

        public bool? Borrado { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual Producto? Producto { get; set; }
    }
}
