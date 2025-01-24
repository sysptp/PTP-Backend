namespace BussinessLayer.DTOs.ModuloCampaña.CmpCampana
{
    public class CmpCampanaCreateDto
    {
        public string? NombreCampana { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int PlantillaId { get; set; }
        public int EmpresaId { get; set; }
    }
}
