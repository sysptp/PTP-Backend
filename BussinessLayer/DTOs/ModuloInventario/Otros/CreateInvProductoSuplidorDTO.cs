
namespace BussinessLayer.DTOs.ModuloInventario.Otros
{
    public class CreateInvProductoSuplidorDTO
    {
        public int ProductoId { get; set; }

        public int SuplidorId { get; set; }
        public long IdEmpresa { get; set; }
        public int IdMoneda { get; set; }

        public decimal? ValorCompra { get; set; }
    }
}
 