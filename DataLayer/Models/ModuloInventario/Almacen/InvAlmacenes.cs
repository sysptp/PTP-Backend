using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvAlmacenes : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public long IdEmpresa { get; set; }
        public int IdMunicipio { get; set; }
        public int IdUsuario { get; set; }
        public long IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool EsPrincipal { get; set; }
 
    }
}
