using BussinessLayer.DTOs.ModuloCampaña.CmpCampana;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.ModuloCampaña.Services
{
    public interface ICmpCampanaService
    {
        Task<Response<CmpCampanaCreateDto>> CreateAsync(CmpCampanaCreateDto dto);
    }
}
