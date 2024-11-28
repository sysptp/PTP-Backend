namespace BussinessLayer.DTOs.ModuloInventario.Pedidos
{
    public class CreateOrderDto
    {

        public long IdEmpresa { get; set; }

        public int? IdSuplidor { get; set; }

        public bool? Solicitado { get; set; }
    }
}
