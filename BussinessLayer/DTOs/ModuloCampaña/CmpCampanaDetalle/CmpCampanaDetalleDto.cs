using BussinessLayer.DTOs.ModuloCampaña.CmpCampana;
using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpCampanaDetalle;

public class CmpCampanaDetalleDto
{
    public long CampanaDetalleId { get; set; }
    public CmpCampanaDto? Campana { get; set; }
    public List<CmpClienteDto> CmpCliente { get; set; }
    public long EmpresaId { get; set; }
}