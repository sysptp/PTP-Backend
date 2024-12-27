using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvAlmacenInventario: AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAlmacen { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadMinima { get; set; }
        public string UbicacionExhibicion { get; set; }
        public string UbicacionGuardada { get; set; }
        public bool Activo { get; set; }
   
    }
}
