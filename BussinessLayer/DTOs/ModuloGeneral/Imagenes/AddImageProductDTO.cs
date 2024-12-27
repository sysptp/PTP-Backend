
namespace BussinessLayer.DTOs.ModuloGeneral.Imagenes
{
    public class AddImageProductDTO
    {
        public long IdEmpresa { get; set; }

        public string? Descripcion { get; set; }

        public string? Url { get; set; }

        public bool? EsPrincipal { get; set; }

        public int? ProductoId { get; set; }

    }
}
