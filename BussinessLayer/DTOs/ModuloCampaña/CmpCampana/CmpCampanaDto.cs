using BussinessLayer.DTOs.ModuloCampaña.CmpEstado;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpCampana;

public class CmpCampanaDto
{
    public string? NombreCampana { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaTermino { get; set; }
    public int EmpresaId { get; set; }
    public CmpEstadoDto CampanaEstado { get; set; } 
}