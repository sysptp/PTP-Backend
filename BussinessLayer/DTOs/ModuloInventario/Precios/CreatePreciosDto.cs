namespace BussinessLayer.DTOs.ModuloInventario.Precios
{
    public class CreatePreciosDto
    {
        public int IdProducto { get; set; }

        public long IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        public decimal PrecioValor { get; set; }

    }
}
