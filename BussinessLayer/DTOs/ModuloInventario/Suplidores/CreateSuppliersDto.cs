namespace BussinessLayer.DTOs.ModuloInventario.Suplidores
{
    public class CreateSuppliersDto
    {
        public long IdEmpresa { get; set; }

        public int? TipoIdentificacion { get; set; }

        public string? NumeroIdentificacion { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? TelefonoPrincipal { get; set; }

        public string? DireccionPrincipal { get; set; }

        public string? Email { get; set; }

        public string? PaginaWeb { get; set; }

        public string? Descripcion { get; set; }
    }
}
