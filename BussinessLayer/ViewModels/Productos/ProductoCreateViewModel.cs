using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BussinessLayer.Validations;
using DataLayer.Models;
using DataLayer.Models.Productos;

namespace BussinessLayer.ViewModels
{
    public class ProductoCreateViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Display(Name = "Codigo")]
        [UniqueProductCode]
        public string Codigo { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Marca Requerida")]
        public int MarcaId { get; set; }

        [Display(Name = "Version")]
        [Required(ErrorMessage = "Version Requerida")]
        public int VersionId { get; set; }

        [Display(Name = "Envase")]
        [Required(ErrorMessage = "Envase Requerido")]
        public int EnvaseId { get; set; }

        [Display(Name = "Es un Lote?")]
        public bool EsLote { get; set; }

        [Display(Name = "Cantidad por Lote")]
        [CantidadRequerido]
        public int? CantidadLote { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Descripcion Requerida")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio Base")]
        [Required(ErrorMessage = "Precio Base Requerido")]
        public decimal PrecioBase { get; set; }

        [Display(Name = "Precio Compra")]
        [Required(ErrorMessage = "Precio Compra Requerido")]
        public decimal PrecioCompra { get; set; }

        [Display(Name = "Cant.Minima")]
        [Required(ErrorMessage = "Cantidad Minima Requerida")]
        public int CantidadMinima { get; set; }

        [Display(Name = "Cantidad Inventario")]
        public int CantidadInventario { get; set; }

        [StringLength(50), Required(ErrorMessage = "Codigo de Barra Requerido")]
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
