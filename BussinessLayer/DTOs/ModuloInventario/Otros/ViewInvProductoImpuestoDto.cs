using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Otros
{
    public class ViewInvProductoImpuestoDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int ImpuestoId { get; set; }
        public string? ImpuestoDescripcion { get; set; }
    }
}
