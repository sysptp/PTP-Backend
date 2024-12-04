using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Otros
{
    public class CreateInvProductoImpuestoDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int ImpuestoId { get; set; }
    }
}
