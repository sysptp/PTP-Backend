using BussinessLayer.DTOs.Otros;


namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvInventarioSucursalReponse:AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
        public string UbicacionExhibicion { get; set; }
        public string UbicacionGuardada { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadMinima { get; set; }
        public bool Activo { get; set; }
    }
}
