using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.ModuloInventario.Descuentos
{
    public class CreateDiscountDto
    {
        public int IdProducto { get; set; }

        public long IdEmpresa { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal? ValorDescuento { get; set; }

        public bool? EsPermanente { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
