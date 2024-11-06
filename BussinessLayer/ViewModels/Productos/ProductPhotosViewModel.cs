using System.Collections.Generic;
using DataLayer.Models;
using DataLayer.Models.ModuloGeneral;

namespace BussinessLayer.ViewModels
{
    public class ProductPhotosViewModel
    {
        public ICollection<Imagen> Imagens { get; set; }

        public int ProductoId { get; set; }

        public int ImagenId { get; set; }

        public int MaxImagenes { get; set; } = 3;
    }
}
