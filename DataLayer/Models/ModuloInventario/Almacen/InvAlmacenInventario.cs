using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvAlmacenInventario: AuditableEntities
    {
        [Key]
        public int Id { get; set; } 
        public int? IdProducto { get; set; } 
        public long? IdEmpresa { get; set; } 
        public int? IdAlmacen { get; set; } 
        public int CantidadProducto { get; set; } 
        public int CantidadMinima { get; set; } 
        public string UbicacionExhibicion { get; set; } = null!;
        public string UbicacionGuardada { get; set; } = null!; 
        public bool Activo { get; set; }

    }
}
