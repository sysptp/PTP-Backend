using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Impuestos
{
    public class CreateTaxDto
    {

        public int? IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        public bool? EsPorcentaje { get; set; }

        public decimal ValorImpuesto { get; set; }

        public string? NombreImpuesto { get; set; }
    }
}
