using BussinessLayer.DTOs.ModuloCampaña.CmpCampanaDetalle;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.ModuloCampaña.Services;

public interface ICmpCampanaDetalleService
{
    Task<Response<List<CmpCampanaDetalleDto>>> GetAll(int empresaId);
    Task<Response<CmpCampanaDetalleDto>> GetById(int empresaId, long id);
}