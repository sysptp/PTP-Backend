using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Precios
{
    public class EditPricesDto
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdMoneda { get; set; }

        public decimal PrecioValor { get; set; }

    }
}
