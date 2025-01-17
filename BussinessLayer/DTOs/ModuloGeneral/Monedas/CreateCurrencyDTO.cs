namespace BussinessLayer.DTOs.ModuloGeneral.Monedas
{
    public class CreateCurrencyDTO
    {
        public int IdPais { get; set; }
        public long IdEmpresa { get; set; }
        public string? Descripcion { get; set; }
        public string? Siglas { get; set; }
        public string? Simbolo { get; set; }
        public bool? EsLocal { get; set; }
        public string? RutaImagen { get; set; }
        public decimal? TasaCambio { get; set; }
    }
}
