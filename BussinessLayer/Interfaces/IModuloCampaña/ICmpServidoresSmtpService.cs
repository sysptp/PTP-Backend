using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.IModuloCampaña
{
    public interface ICmpServidoresSmtpService
    {
        Task<Response<CmpServidoresSmtpCreateDto>> CreateAsync(CmpServidoresSmtpCreateDto cmpServidoresSmtpCreateDto);
        Task<Response<List<CmpServidoresSmtpDto>>> GetAllAsync();
        Task<Response<CmpServidoresSmtpDto>> GetByIdAsync(int id);
        Task<Response<CmpServidoresSmtpUpdateDto>> UpdateAsync(CmpServidoresSmtpUpdateDto cmpServidoresSmtpUpdateDto);
        Task<Response<bool>> DeleteAsync(int id);



    }
}
