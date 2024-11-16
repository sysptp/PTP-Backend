using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Pedidos
{
    public class EditOrderDto
    {
        public int Id { get; set; }

        public int? IdSuplidor { get; set; }

        public bool? Solicitado { get; set; }
    }
}
