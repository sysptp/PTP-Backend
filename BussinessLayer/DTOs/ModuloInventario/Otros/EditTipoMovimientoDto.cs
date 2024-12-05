using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Otros
{
    public class EditTipoMovimientoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool IN_OUT { get; set; }
    }
}
