using BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas
{
    public class CmpPlantillaDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Contenido { get; set; }
        public bool EsHtml { get; set; }
        public int EmpresaId { get; set; }
        public int TipoPlantillaId { get; set; }
        public CmpTipoPlantillaDto CmpTipoPlantilla { get; set; }
    }
}
