using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.ViewModels
{
    public class ProductoPrecioInfoViewModel
    {
        [DisplayName("Nombre de Producto")]
        public string Nombre { get; set; }
        [DisplayName("Producto"), Required(ErrorMessage = "Producto Requerido")]
        public int ProductoId { get; set; }
        [DisplayName("Precio Base")]
        public decimal PrecioBase { get; set; }
        [DisplayName("Precio #1")]
        public decimal Precio1 { get; set; }
        [DisplayName("Precio #2")]
        public decimal Precio2 { get; set; }
        [DisplayName("Precio #3")]
        public decimal Precio3 { get; set; }
    }
}