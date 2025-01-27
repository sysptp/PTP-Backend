using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCampanaDetalle;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Services.SModuloCampaña;

public class CmpCampanaDetalleService(ICmpCampanaDetalleRepository repository,IMapper mapper) : ICmpCampanaDetalleService
{
    public async Task<Response<List<CmpCampanaDetalleDto>>> GetAll(int empresaId)
    {
        try
        {
            List<CmpCampanaDetalle> campanas = await repository.GetAll(empresaId);
            if(campanas.Count <= 0) return Response<List<CmpCampanaDetalleDto>>.NoContent();
            
            List<CmpCampanaDetalleDto> campanasDto = mapper.Map<List<CmpCampanaDetalleDto>>(campanas);
            return Response<List<CmpCampanaDetalleDto>>.Success(campanasDto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Task<Response<CmpCampanaDetalleDto>> GetById(int empresaId, long id)
    {
        throw new NotImplementedException();
    }
}