using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.ModuloCampaña.Services
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
