using DataLayer.Models.Otros;

namespace BussinessLayer.DTOs.Precios
{
    public class ViewPreciosDto : BaseModel
    {
        public int ProductoId { get; set; }
        public decimal Valor { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Borrado { get; set; }

    }
}
