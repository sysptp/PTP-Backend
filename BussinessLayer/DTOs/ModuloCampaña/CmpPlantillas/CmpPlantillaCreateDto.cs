namespace BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas
{
    public class CmpPlantillaCreateDto
    {
        public string? Nombre { get; set; }
        public string? Contenido { get; set; }
        public string? UsuarioAdicion { get; set; }
        public long EmpresaId { get; set; }
        public int TipoPlantillaId { get; set; }
        public bool EsHtml { get; set; }
    }
}
