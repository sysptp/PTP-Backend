using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Precios
{
    public class SetSamePriceDTO
    {
        public List<EditPricesDto> editPricesDtos {  get; set; }

        public decimal NewPrice { get; set; }
    }
}
