using DataLayer.Models.Otros;
using DataLayer.Models.Productos;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Productos
{
    public class CreateProductsDto : BaseModel
    {
        public string? Codigo { get; set; }

        public int IdVersion { get; set; }

        public int IdSuplidor { get; set; }

        public int IdEnvase { get; set; }

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        public bool EsLote { get; set; }

        public int CantidadLote { get; set; }

        public string? Imagen { get; set; }

        public bool AdmiteDescuento { get; set; } = true;

        public bool HabilitaVenta { get; set; } = true;

        public string? EsProducto { get; set; }

        public int CantidadInventario { get; set; }

        public string? AplicaImp { get; set; }

        public decimal ValorImpuesto { get; set; }

        public decimal PrecioBase { get; set; }

        public decimal PrecioCompra { get; set; }

        public int CantidadMinima { get; set; }

        public string? CodigoBarra { get; set; }
    }
}
