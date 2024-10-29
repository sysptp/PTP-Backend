using System;
using System.Collections.Generic;
using DataLayer.Models;
using DataLayer.Models.Facturas;

namespace BussinessLayer.ViewModels
{
    public class FacturacionViewModel
    {
        public Facturacion Facturacion { get; set; }

        public List<DetalleFacturacion> DetalleFacturacions { get; set; }

        public DateTime FechaLimite { get; set; }

        public decimal MontoInicial { get; set; }

    }
}
