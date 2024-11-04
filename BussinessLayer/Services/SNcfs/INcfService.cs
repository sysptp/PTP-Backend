using BussinessLayer.DTOs.Ncfs;

namespace BussinessLayer.Services.SNcfs
{
    public interface INcfService
    {
        Task CreateAsync(CreateNcfDto createNcfDto);

        Task<List<NcfDto>> GetAllAsync(int bussines);
        Task<NcfDto> GetByIdAsync(int bussines,string ncfType);
        Task DeleteAsync(int bussines,string ncfType);
    }
}
