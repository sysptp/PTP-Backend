using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataLayer.Models;
using DataLayer.Models.Productos;

namespace BussinessLayer.ViewModels 
{ 
    public class ProductoInfoViewModel
    {
        public int Id { get; set; } 
        public string Marca { get; set; }
        public string Version { get; set; }
        [DisplayName("Nombre")]
        public string NombreCompleto { get; set; } 
        public string Codigo { get; set; }
        public string Envase { get; set; }

        public string Descripcion { get; set; }
        [DisplayName("Es Lote?")]
        public bool EsLote { get; set; }
        [DisplayName("Cantidad Lote")]
        public int CantidadLote { get; set; }
        [DisplayName("Inventario")]
        public int Stock { get; set; }
        [DisplayName("Cantidad Minima")]
        public int CantidadMinima { get; set; }
        [DisplayName("Precio Compra")]
        public decimal PrecioCompra { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        public ICollection<Descuentos> Descuentos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public bool Activo { get; set; }

        public long IdEmpresa { get; set; }

        public string Imagen { get; set; }
        public bool AdmiteDescuento { get; set; } = true;
        public bool HabilitaVenta { get; set; } = true;
        public string EsProducto { get; set; }

    }
}