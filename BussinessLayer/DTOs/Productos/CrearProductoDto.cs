using BussinessLayer.Validations;
using DataLayer.Models.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Productos
{
    public class CrearProductoDto
    {
        public int? Id { get; set; }

        public bool Activo { get; set; }

        [UniqueProductCode]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Marca Requerida")]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "Version Requerida")]
        public int VersionId { get; set; }

        [Required(ErrorMessage = "Envase Requerido")]
        public int EnvaseId { get; set; }

        public bool EsLote { get; set; }

        [CantidadRequerido]
        public int? CantidadLote { get; set; }

        [Required(ErrorMessage = "Descripcion Requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Precio Base Requerido")]
        public decimal PrecioBase { get; set; }

        [Required(ErrorMessage = "Precio Compra Requerido")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "Cantidad Minima Requerida")]
        public int CantidadMinima { get; set; }

        public int CantidadInventario { get; set; }

        public string CodigoBarra { get; set; }

        public ICollection<Imagen> Imagenes { get; set; }

        public ICollection<Descuentos> Descuentos { get; set; }

        public long IdEmpresa { get; set; }

        public string Imagen { get; set; }

        public bool AdmiteDescuento { get; set; } = true;

        public bool HabilitaVenta { get; set; } = true;

        public string EsProducto { get; set; }

        public string AplicaImp { get; set; }

        public decimal ValorImpuesto { get; set; }
    }
}
