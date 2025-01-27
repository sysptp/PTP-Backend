namespace BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas
{
    public class CmpPlantillaUpdateDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Contenido { get; set; }
        public string? UsuarioModificacion{ get; set; }

        public bool EsHtml { get; set; }
        public long EmpresaId { get; set; }
        public int TipoPlantillaId { get; set; }
    }
}
