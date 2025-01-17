
namespace BussinessLayer.DTOs.ModuloGeneral.Monedas
{
    public class ViewCurrencyDTO
    {
        public int? Id { get; set; }

        public int? IdPais { get; set; }

        public long? IdEmpresa { get; set; }

        public string? Descripcion { get; set; }

        public string? Siglas { get; set; }

        public string? Simbolo { get; set; }

        public bool? EsLocal { get; set; }

        public decimal? TasaCambio { get; set; }

        public bool? Borrado { get; set; }

        public bool? Activo { get; set; }
        public string? RutaImagen { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
