using BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.ModuloCampaña.Services
{
    public interface ICmpPlantillaService
    {
        Task<Response<CmpPlantillaCreateDto>> CreateAsync(CmpPlantillaCreateDto createDto);
        Task<Response<List<CmpPlantillaDto>>> GetAllAsync(int empresaId);
        Task<Response<CmpPlantillaDto>> GetByIdAsync(int id,int empresaId);
        Task<Response<CmpPlantillaUpdateDto>> UpdateAsync(CmpPlantillaUpdateDto updateDto);
    }
}
