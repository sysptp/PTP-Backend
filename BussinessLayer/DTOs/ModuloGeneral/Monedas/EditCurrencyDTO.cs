using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloGeneral.Monedas
{
    public class EditCurrencyDTO
    {
        public int? Id { get; set; }

        public int? IdPais { get; set; }

        public string? Descripcion { get; set; }

        public string? Siglas { get; set; }

        public string? Simbolo { get; set; }

        public bool? Activo { get; set; }

        public bool? EsLocal { get; set; }

        public decimal? TasaCambio { get; set; }

    }
}
