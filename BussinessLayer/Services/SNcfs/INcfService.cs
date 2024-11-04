using BussinessLayer.DTOs.Ncfs;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.SNcfs
{
    public interface INcfService
    {
        Task<Response<CreateNcfDto>> CreateAsync(CreateNcfDto createNcfDto);
        Task<Response<List<NcfDto>>> GetAllAsync(int bussines);
        Task<Response<NcfDto>> GetByIdAsync(int bussines, string ncfType);
        Task<Response<string>> DeleteAsync(int bussines, string ncfType);
    }
}
