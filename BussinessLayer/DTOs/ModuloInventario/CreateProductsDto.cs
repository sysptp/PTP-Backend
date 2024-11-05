using DataLayer.Models.ModuloGeneral;
using DataLayer.Models.ModuloInventario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.ModuloInventario
{
    public class CreateProductsDto 
    {   
        public long? IdEmpresa { get; set; }

        public int? IdVersion { get; set; }

        public int? IdTipoProducto { get; set; }

        public string? CodigoBarra { get; set; }

        public string? Codigo { get; set; }

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }

        public int? CantidadLote { get; set; }

        public int? CantidadInventario { get; set; }

        public int? CantidadMinima { get; set; }

        public bool? AdmiteDescuento { get; set; }

        public bool? HabilitaVenta { get; set; }

        public bool? AplicaImpuesto { get; set; }

        public bool? EsLote { get; set; }

        public bool? EsProducto { get; set; }

        public bool? EsLocal { get; set; }
    }
}
