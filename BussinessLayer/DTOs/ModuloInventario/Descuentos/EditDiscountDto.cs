namespace BussinessLayer.DTOs.ModuloInventario.Descuentos
{
    public class EditDiscountDto
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal? ValorDescuento { get; set; }

        public bool? EsPermanente { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

    }
}
