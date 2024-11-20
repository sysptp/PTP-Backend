using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Descuentos
{
    public class CreateDiscountDto
    {
        public int? Id { get; set; }

        public int? IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal? ValorDescuento { get; set; }

        public bool? EsPermanente { get; set; }

        public bool? Activo { get; set; }
    }
}
