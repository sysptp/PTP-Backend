using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Otros
{
    public class ViewInvProductoSuplidorDTO
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public int SuplidorId { get; set; }
        public long IdEmpresa { get; set; }

        public int IdMoneda { get; set; }

        public decimal? ValorCompra { get; set; }
    }
}
