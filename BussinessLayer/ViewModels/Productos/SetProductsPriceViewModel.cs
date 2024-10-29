using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.ViewModels
{
    public class SetProductsPriceViewModel
    {
        public int Id { get; set; }

        public int[] ProductsIdList { get; set; }

        [DisplayName("Precio Nuevo")]
        [Required(ErrorMessage = "Ingrese Precio")]
        public int PriceBase { get; set; }
    }
}
